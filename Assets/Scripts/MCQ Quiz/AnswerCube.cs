using UnityEngine;


/// <summary>
/// Attached to each of the answer cubes. Set OnPulled() as a triggered script on SceneInteractor.
/// </summary>
public class AnswerCube : QuizCube
{
	[Tooltip("0 = A, 1 = B, 2 = C, 3 = D, ...")]
	public int answerIndex = -1;

	float lastPullTime = -1f;
	float pullCooldown = 0.25f;




	protected override void Awake()
	{
		base.Awake();

		if (answerIndex < 0)
		{
			Debug.LogWarning("AnswerCube on " + name + " does not have an assigned index. Set associated answer index in inspector follwing the pattern 0 = A, 1 = B, 2 = C, ...");
		}
	}


	// Called by SceneInteractor when this cube is pulled.
	public override void OnPulled()
	{
		if (Time.time - lastPullTime < pullCooldown) // Need to delay if not unity calls this twice
			return;

		lastPullTime = Time.time;

		quizManager.OnAnswerCubePulled(this);
	}





	public void SetGreen()
	{
		cubeRenderer.material.color = Color.green;
	}



	public void SetRed()
	{
		cubeRenderer.material.color = Color.red;
	}
}