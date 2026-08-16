using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public enum ALType { OneMM, PointTwoMM }

[RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
public class ALSheet : MonoBehaviour
{
	public ALType aLType;
	public float thicknessMM = 1f;
	public LayerMask zoneLayerMask;
	public float overlapCheckRadius = 0.03f;

	private XRGrabInteractable grabInteractable;
	private Rigidbody rb;

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



	public void SetGrabbable(bool value) => grabInteractable.enabled = value;



	public void SetCurrentZone(PlacementZone zone) => CurrentZone = zone;



	private void OnGrabbed(SelectEnterEventArgs args)
	{
		CurrentZone?.RemoveObject(this);
		CurrentZone = null;

	}



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