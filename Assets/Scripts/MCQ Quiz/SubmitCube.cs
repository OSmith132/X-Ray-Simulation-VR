using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Attached to the one submit cube. Set OnPulled() as a triggered script on SceneInteractor.
/// </summary>
public class SubmitCube : MonoBehaviour
{
    [SerializeField] QuizManager quizManager;

    Renderer cubeRenderer;
    Color defaultColour;

    
    void Awake()
    {
        cubeRenderer = GetComponent<Renderer>();
        defaultColour = cubeRenderer.material.color;
    }



    // Called by the SceneInteractor when this cube is pulled.
    public void OnPulled()
    {
        // in case this fires while the interactable is meant to be disabled.
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        if (grab != null && !grab.enabled) { return; }

        quizManager.OnSubmitCubePulled();
    }




    // Disabled = grey and not grabbable. Enabled = default colour and grabbable.
    public void SetInteractable(bool canBePulled)
    {
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = canBePulled;
        }

        cubeRenderer.material.color = canBePulled ? defaultColour : Color.grey;
    }



    public void SetGrey()
    {
        cubeRenderer.material.color = Color.grey;
    }
}
