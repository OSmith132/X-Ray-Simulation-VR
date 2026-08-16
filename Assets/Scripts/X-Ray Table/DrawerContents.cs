using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach to the drawer's cavity collider (ensure it has a collider with 'Is trigger' ticked)
///
/// Items entering the drawere are left dynamic for a short time so gravity can let them land naturally in the drawer. Once an item's velocity has stayed low
/// for settleTime seconds, it is locked (made kinematic and made to hold a fixed offset relative to the drawer. Adding as a child of the drawer doesn't work here for scaling issues.
/// Grabbing an item using XR Interaction Toolkit removes the item from the drawer.
/// </summary>
/// 
[RequireComponent(typeof(Collider))]
public class DrawerContents : MonoBehaviour
{

	[Tooltip("Only objects with this tag count as drawer contents. Leave blank to accept anything (with a Rigidbody).")]
	public string requiredTag = "";

	[Tooltip("How long in seconds an item's velocity must stay below the thresholds before it's in to the drawer.")]
	public float settleTime = 0.15f; // This worked well for me

	[Tooltip("Linear speed below which an item counts as not moving.")]
	public float linearSettleThreshold = 0.05f;

	[Tooltip("Angular speed below which an item counts as not moving.")]
	public float angularSettleThreshold = 5f;

	[Tooltip("force-lock an item after this many seconds.")]
	public float maxSettleWaitTime = 0.5f;

	[Tooltip("How long an OnTriggerExit must persist before it's treated as real, to filter out boundary jitter.")]
	public float exitGracePeriod = 0.1f;




	private enum State { Settling, Locked }

	private class ItemState
	{
		public State state;
		public float settledFor;
		public float totalWaitTime;
		public Vector3 localPosition;
		public Quaternion localRotation;
		public bool pendingExit;
		public float pendingExitTimer;
	}

	// Tracks a held item so it can be moved past other items sitting in the drawer instead of getting stuck against them.
	private class HeldIgnoreEntry
	{
		public Rigidbody heldRb;
		public List<Collider> heldColliders;
		public List<Collider> otherColliders;
	}



	private Dictionary<Rigidbody, ItemState> trackedItems = new Dictionary<Rigidbody, ItemState>();
	private List<Rigidbody> toRemove = new List<Rigidbody>();
	private List<HeldIgnoreEntry> activeIgnores = new List<HeldIgnoreEntry>();



	private static bool IsHeldByPlayer(Rigidbody rb)
	{
		UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = rb.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
		return grab != null && grab.isSelected;
	}





	private void BeginIgnoringHeld(Rigidbody heldRb)
	{
		foreach (var existing in activeIgnores)
		{
			if (existing.heldRb == heldRb) return; // already set up
		}

		List<Collider> heldColliders = new List<Collider>(heldRb.GetComponentsInChildren<Collider>());
		List<Collider> otherColliders = new List<Collider>();

		foreach (var kvp in trackedItems)
		{
			if (kvp.Key == heldRb) continue;
			otherColliders.AddRange(kvp.Key.GetComponentsInChildren<Collider>());
		}

		foreach (var hc in heldColliders)
		{
			foreach (var oc in otherColliders)
			{
				Physics.IgnoreCollision(hc, oc, true);
			}
		}

		activeIgnores.Add(new HeldIgnoreEntry
		{
			heldRb = heldRb,
			heldColliders = heldColliders,
			otherColliders = otherColliders
		});
	}

	// Restores collision for a HeldIgnoreEntry once the item is released.
	private void EndIgnoringHeld(HeldIgnoreEntry entry)
	{
		foreach (var hc in entry.heldColliders)
		{
			if (hc == null) continue;
			foreach (var oc in entry.otherColliders)
			{
				if (oc == null) continue;
				Physics.IgnoreCollision(hc, oc, false);
			}
		}
	}



	private void OnTriggerEnter(Collider other)
	{
		Rigidbody rb = other.attachedRigidbody;
		if (rb == null) return;

		bool heldByPlayer = IsHeldByPlayer(rb);

		if (trackedItems.TryGetValue(rb, out ItemState existing))
		{

			existing.pendingExit = false;
			existing.pendingExitTimer = 0f;
			return;
		}

		if ((!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag)) ||
		   (heldByPlayer))
		{
			if (heldByPlayer) BeginIgnoringHeld(rb); // held item passing through the drawer should still ignore its contents
			return;
		}

		Debug.Log("enter drawer");


		// Leave it dynamic so it can fall naturally.
		trackedItems[rb] = new ItemState
		{
			state = State.Settling,
			settledFor = 0f,
			totalWaitTime = 0f,
			pendingExit = false,
			pendingExitTimer = 0f
		};

		// If something else is currently being held make sure this item ignores collision with it
		foreach (var entry in activeIgnores)
		{
			if (entry.heldRb == rb) continue;
			Collider[] newCols = rb.GetComponentsInChildren<Collider>();
			foreach (var c in newCols)
			{
				entry.otherColliders.Add(c);
				foreach (var hc in entry.heldColliders)
				{
					Physics.IgnoreCollision(hc, c, true);
				}
			}
		}
	}




	private void OnTriggerExit(Collider other)
	{
		Rigidbody rb = other.attachedRigidbody;
		if (rb == null) return;

		if (IsHeldByPlayer(rb))
		{
			trackedItems.Remove(rb);
			return;
		}

		if (!trackedItems.TryGetValue(rb, out ItemState state)) return;


		state.pendingExit = true;
		state.pendingExitTimer = 0f;

		// If it left while still Settling there's nothing to undo.
	}





	private void FixedUpdate()
	{
		// Release collision-ignore for anything that's no longer being held.
		for (int i = activeIgnores.Count - 1; i >= 0; i--)
		{
			HeldIgnoreEntry entry = activeIgnores[i];
			if (entry.heldRb == null || !IsHeldByPlayer(entry.heldRb))
			{
				EndIgnoringHeld(entry);
				activeIgnores.RemoveAt(i);
			}
		}

		if (trackedItems.Count == 0) return;

		toRemove.Clear();


		foreach (var kvp in trackedItems)
		{

			Rigidbody rb = kvp.Key;
			ItemState state = kvp.Value;

			if (rb == null)
			{
				toRemove.Add(rb);
				continue;
			}



			if (IsHeldByPlayer(rb))
			{
				BeginIgnoringHeld(rb); // let it pass through/move other drawer contents while dragged
				if (state.state == State.Locked) // Shouldn't ever really happen
				{
					rb.isKinematic = false;
				}
				toRemove.Add(rb);
				continue;
			}



			if (state.pendingExit)
			{
				state.pendingExitTimer += Time.fixedDeltaTime;
				if (state.pendingExitTimer >= exitGracePeriod)
				{
					Debug.Log("exit drawer");
					if (state.state == State.Locked)
					{
						rb.isKinematic = false;
					}
					toRemove.Add(rb);
					continue;
				}

			}



			if (state.state == State.Settling)
			{
				bool settled = rb.linearVelocity.sqrMagnitude <= linearSettleThreshold * linearSettleThreshold &&
							   rb.angularVelocity.sqrMagnitude <= (angularSettleThreshold * Mathf.Deg2Rad) * (angularSettleThreshold * Mathf.Deg2Rad);




				state.settledFor = settled ? state.settledFor + Time.fixedDeltaTime : 0f;

				state.totalWaitTime += Time.fixedDeltaTime;


				bool forceLock = state.totalWaitTime >= maxSettleWaitTime;
				if (state.settledFor >= settleTime || forceLock)
				{
					state.localPosition = transform.InverseTransformPoint(rb.position);
					state.localRotation = Quaternion.Inverse(transform.rotation) * rb.rotation;
					rb.linearVelocity = Vector3.zero;
					rb.angularVelocity = Vector3.zero;
					rb.isKinematic = true;
					state.state = State.Locked;
				}
				else
				{
					continue; // still falling
				}
			}



			// Locked
			Vector3 targetPos = transform.TransformPoint(state.localPosition);
			Quaternion targetRot = transform.rotation * state.localRotation;
			rb.MovePosition(targetPos);
			rb.MoveRotation(targetRot);

			Debug.Log("Locked!");
		}


		foreach (Rigidbody rb in toRemove)
		{
			trackedItems.Remove(rb);
		}


	}








	private void OnDisable()
	{
		foreach (var kvp in trackedItems)
		{
			if (kvp.Key == null) continue;
			if (IsHeldByPlayer(kvp.Key)) continue;
			if (kvp.Value.state == State.Locked)
			{
				kvp.Key.isKinematic = false;
			}
		}
		trackedItems.Clear();

		foreach (var entry in activeIgnores)
		{
			EndIgnoringHeld(entry);
		}
		activeIgnores.Clear();
	}

}