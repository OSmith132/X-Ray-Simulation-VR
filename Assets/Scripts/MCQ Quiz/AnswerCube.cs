using UnityEngine;


/// <summary>
/// Attached to each of the four answer cubes. Set OnPulled() as a triggered script on SceneInteractor.
/// </summary>
public class AnswerCube : MonoBehaviour
{
    [Tooltip("0 = A, 1 = B, 2 = C, 3 = D, ...")]
    public int answerIndex = -1;

    [SerializeField] QuizManager quizManager;

    Renderer cubeRenderer;
    Color defaultColour;

	float lastPullTime = -1f;
	float pullCooldown = 0.25f;

	void Awake()
    {
        cubeRenderer = GetComponent<Renderer>();
        defaultColour = cubeRenderer.material.color;

        if (answerIndex < 0)
        {
			Debug.LogWarning("AnswerCube on " + name + " does not have an assigned index. Set associated answer index in inspector follwing the pattern 0 = A, 1 = B, 2 = C, ...");
		}
    }


    // Called by SceneInteractor when this cube is pulled.
	public void OnPulled()
	{
		if (Time.time - lastPullTime < pullCooldown) // Need to delay if not unity calls this twice
			return;

		lastPullTime = Time.time;

		quizManager.OnAnswerCubePulled(this);
	}

	
    
    
    
    public void SetDefault()
    {
        cubeRenderer.material.color = defaultColour;
    }


    public void SetGreen()
    {
        cubeRenderer.material.color = Color.green;
    }



    public void SetRed()
    {
        cubeRenderer.material.color = Color.red;
    }






    public void SetGrey()
    {
        cubeRenderer.material.color = Color.grey;
    }


    // Used to lock the cube once the quiz has finished.
    public void SetInteractable(bool canBePulled)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = canBePulled;
        }
    }
}
