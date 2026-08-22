using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;



/// <summary>
/// Abstract base for any grabbable object that can be placed into and removed from a PlacementZone.
/// Handles grabbing/releasing and finding the nearest valid zone on release.
/// </summary>
[RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
public abstract class Stackable : MonoBehaviour
{
	public LayerMask zoneLayerMask;
	public float overlapCheckRadius = 0.03f;
	public float thicknessMM = 1f;

	protected XRGrabInteractable grabInteractable;
	protected Rigidbody rb;

	public PlacementZone CurrentZone { get; private set; }




	private void Awake()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		rb = GetComponent<Rigidbody>();

		grabInteractable.selectEntered.AddListener(OnGrabbed);
		grabInteractable.selectExited.AddListener(OnReleased);
	}

	private void OnDestroy()
	{
		grabInteractable.selectEntered.RemoveListener(OnGrabbed);
		grabInteractable.selectExited.RemoveListener(OnReleased);
	}



	/// <summary>
	/// Enables or disables grabbing of this object.
	/// </summary>
	public void SetGrabbable(bool value) => grabInteractable.enabled = value;



	/// <summary>
	/// Sets the zone this object currently belongs to.
	/// </summary>
	public void SetCurrentZone(PlacementZone zone) => CurrentZone = zone;



	/// <summary>
	/// Called when the object is grabbed. Removes it from its current zone, if there is one.
	/// </summary>
	private void OnGrabbed(SelectEnterEventArgs args)
	{
		CurrentZone?.RemoveObject(this);
		CurrentZone = null;
	}



	/// <summary>
	/// Called when the object is released. Finds the nearest overlapping zone that will accept it and places it there, otherwise lets it fall under gravity.
	/// </summary>
	private void OnReleased(SelectExitEventArgs args)
	{
		Collider[] hits = Physics.OverlapSphere(transform.position, overlapCheckRadius, zoneLayerMask);

		PlacementZone bestZone = null;
		float bestDist = float.MaxValue;

		foreach (var hit in hits)
		{
			var zone = hit.GetComponentInParent<PlacementZone>();
			if (zone == null || !zone.CanAccept(this)) continue;

			float d = Vector3.Distance(transform.position, hit.transform.position);
			if (d < bestDist)
			{
				bestDist = d;
				bestZone = zone;
			}
		}

		if (bestZone != null)
		{
			bestZone.PlaceObject(this);
		}
		else
		{
			rb.isKinematic = false;
			rb.useGravity = true;
		}
	}
}