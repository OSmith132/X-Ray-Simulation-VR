using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody))]
public class SceneInteractor : MonoBehaviour
{
	[Header("Anchor")]
	[SerializeField, Tooltip("Reference transform the interactor snaps back to")] Transform homeAnchor;
	[SerializeField, Tooltip("How far to pull interactor before firing")] float triggerDistance = 0.1f;

	[Header("Scene Behaviour")]
	[SerializeField, Tooltip("Enable to load a new scene")] bool loadScene;
	[SerializeField, Tooltip("The new scene to traverse to")] string sceneToLoad;
	[SerializeField, Tooltip("Reset the current scene")] bool reloadCurrentScene;

	[Header("Player Teleport")]
	[SerializeField, Tooltip("Enable to teleport the player")] bool teleportPlayer;
	[SerializeField, Tooltip("Reference to the player rig's XROrigin component")] XROrigin xrOrigin;
	[SerializeField, Tooltip("Where the player will be teleported to")] Transform playerDestination;
	[SerializeField, Tooltip("Also move any objects currently held by the player's controllers")] bool teleportHeldObjects = true;

	[Header("Extra Effects")]
	[SerializeField, Tooltip("Runs a script for adition")] UnityEvent onTriggered;

	XRGrabInteractable grabInteractable;
	Rigidbody rb;
	bool hasTriggered;

	void Awake()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		rb = GetComponent<Rigidbody>();
	}

	void OnEnable()
	{
		if (grabInteractable != null)
		{
			grabInteractable.selectEntered.AddListener(OnGrabbed);
		}
	}

	void OnDisable()
	{
		if (grabInteractable != null)
		{
			grabInteractable.selectEntered.RemoveListener(OnGrabbed);
		}
	}

	void OnGrabbed(SelectEnterEventArgs args)
	{
		hasTriggered = false;
	}

	void Update()
	{
		if (hasTriggered || homeAnchor == null) return;

		float distance = Vector3.Distance(transform.position, homeAnchor.position);
		if (distance < triggerDistance) return;

		hasTriggered = true;
		Fire();
		ResetCube();
	}

	void Fire()
	{
		if (teleportPlayer && xrOrigin != null && playerDestination != null)
		{
			// Move held objects the same distance
			Vector3 moved_dist = playerDestination.position - xrOrigin.Camera.transform.position;

			if (teleportHeldObjects)
			{
				TeleportHeldObjects(moved_dist);
			}

			xrOrigin.MoveCameraToWorldLocation(playerDestination.position);
		}

		if (loadScene && !string.IsNullOrEmpty(sceneToLoad))
		{
			SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
		}
		else if (reloadCurrentScene)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		}

		onTriggered?.Invoke();
	}

	void TeleportHeldObjects(Vector3 delta)
	{
		if (xrOrigin == null) return;

		// Find every interactor (controller) and move whatever it's holding.
		var interactors = xrOrigin.GetComponentsInChildren<XRBaseInteractor>();

		foreach (var interactor in interactors)
		{
			if (interactor is not IXRSelectInteractor selectInteractor) continue;

			foreach (var interactable in selectInteractor.interactablesSelected)
			{
				if (interactable == null) continue;

				if ((object)interactable == grabInteractable) continue; // don't move the cube

				Transform heldTransform = interactable.transform;
				Rigidbody heldRb = heldTransform.GetComponent<Rigidbody>();

				heldTransform.position += delta;

				if (heldRb != null)
				{
					heldRb.linearVelocity = Vector3.zero;
					heldRb.angularVelocity = Vector3.zero;
				}
			}
		}
	}







	void ResetCube()
	{
		if (grabInteractable != null && grabInteractable.isSelected)
		{
			grabInteractable.interactionManager.CancelInteractableSelection(
				(IXRSelectInteractable)grabInteractable);
		}



		hasTriggered = false;  // Only if we don't mind the cube being knocked


		// UNCOMMENT IF NOT USING A CONFIGURABLE JOINT TO RETURN TO ORIGIN!!!!

		//if (rb != null)
		//{
		//	rb.linearVelocity = Vector3.zero;
		//	rb.angularVelocity = Vector3.zero;
		//}

		//transform.position = homeAnchor.position;
		//transform.rotation = homeAnchor.rotation;
	}
}