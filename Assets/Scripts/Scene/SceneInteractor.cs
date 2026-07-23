using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;



/// <summary>
/// Generic class used for teleporting the player, changing scene, resetting the scene,
/// or triggering an arbitrary event, once it has been dragged a set distance from its home anchor.
/// Replaces the per-cube distance checks previously duplicated across DetectTouch.Update().
/// Attach to each cube; configure the behaviour toggles per instance in the Inspector.
/// </summary>
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
	[SerializeField, Tooltip("Reference to the player rig (e.g. XROrigin)")] Transform playerRig;
	[SerializeField, Tooltip("Where the player will be teleported to")] Transform playerDestination;

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
		// Allow the cube to fire again the next time it's picked up and pulled.
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
		if (teleportPlayer && playerRig != null && playerDestination != null)
		{
			playerRig.position = playerDestination.position;

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






	void ResetCube()
	{
		if (grabInteractable != null && grabInteractable.isSelected)
		{
			grabInteractable.interactionManager.CancelInteractableSelection(
				(IXRSelectInteractable)grabInteractable);
		}



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