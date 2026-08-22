using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Attach to any grabbable object to ensure it will be affected by gravity on grab.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class GrabRestoresPhysics : MonoBehaviour
{
	Rigidbody rb;
	Collider col;
	XRGrabInteractable grabInteractable;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		grabInteractable = GetComponent<XRGrabInteractable>();

		grabInteractable.selectEntered.AddListener(OnGrabbed);
		grabInteractable.selectExited.AddListener(OnReleased);
	}

	void OnDestroy()
	{
		if (grabInteractable != null)
		{
			grabInteractable.selectEntered.RemoveListener(OnGrabbed);
			grabInteractable.selectExited.RemoveListener(OnReleased);
		}
	}

	void OnGrabbed(SelectEnterEventArgs args)
	{
		RestorePhysics();
	}

	void OnReleased(SelectExitEventArgs args)
	{
		RestorePhysics();
	}

	void RestorePhysics()
	{
		rb.isKinematic = false;
		rb.useGravity = true;

		if (col != null && !col.enabled)
		{
			col.enabled = true; // re-enable if it was disabled while settled
		}
	}
}