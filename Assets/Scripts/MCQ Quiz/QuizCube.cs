using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Base class for the cubes used by the quiz. Handles the colour and interactable state.
/// Derived classes implement OnPulled()
/// </summary>
public abstract class QuizCube : MonoBehaviour
{
	[SerializeField] protected QuizManager quizManager;

	protected Renderer cubeRenderer;
	protected Color defaultColour;





	// Called by SceneInteractor when this cube is pulled.
	public abstract void OnPulled();






	protected virtual void Awake()
	{
		cubeRenderer = GetComponent<Renderer>();
		defaultColour = cubeRenderer.material.color;
	}


	public void SetDefault()
	{
		cubeRenderer.material.color = defaultColour;
	}



	public void SetGrey()
	{
		cubeRenderer.material.color = Color.grey;
	}


	// Disabled = grey and not grabbable. Enabled = default colour and grabbable.
	public virtual void SetInteractable(bool canBePulled)
	{
		XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
		if (grab != null)
		{
			grab.enabled = canBePulled;
		}

		cubeRenderer.material.color = canBePulled ? defaultColour : Color.grey;
	}
}