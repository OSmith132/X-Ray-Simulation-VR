using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Attached to the submit cube. Set OnPulled() as a triggered script on SceneInteractor.
/// </summary>
public class SubmitCube : QuizCube
{
	// Called by the SceneInteractor when this cube is pulled.
	public override void OnPulled()
	{
		// in case this fires while the interactable is meant to be disabled.
		XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
		if (grab != null && !grab.enabled) { return; }

		quizManager.OnSubmitCubePulled();
	}
}