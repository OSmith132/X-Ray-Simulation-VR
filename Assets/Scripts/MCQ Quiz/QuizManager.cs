using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the multiple choice quiz. Reads the questions from the given text file, displays them on the board, tracks the
/// score, and tells the answer/submit cubes when to change colour.
///
/// </summary>
public class QuizManager : MonoBehaviour
{
	[Header("Question Source")]
	[Tooltip("Plain text file with questions, assigned from the Project window. If left empty, a 2 question placeholder quiz is used instead.")]
	[SerializeField] TextAsset questionsFile;

	[Header("Cubes (wire these up in the Inspector)")]
	[Tooltip("The 4 answer cube scripts, in A, B, C, D, ... order.")]
	[SerializeField] AnswerCube[] answerCubes = new AnswerCube[4];
	[SerializeField] SubmitCube submitCube;

	TextMeshPro questionText;      // Found on the 'Question Text' board object
	TextMeshPro instructionMessage; // Found on the 'Instruction Message' board object

	enum QuizState
	{
		NotStarted,
		Answering,
		Feedback,
		Finished
	}

	QuizState state;


	List<Question> questions = new List<Question>();

	int currentQuestionIndex = 0;
	int correctCount = 0;
	int selectedAnswerIndex = -1; // -1 = nothing selected yet




	void Start()
	{
		questionText = GameObject.Find("Question Text").GetComponent<TextMeshPro>();
		instructionMessage = GameObject.Find("Instruction Message").GetComponent<TextMeshPro>();

		LoadQuestions();

		state = QuizState.NotStarted;
		questionText.text = "";
		instructionMessage.text = "Pull any cube to start";

		ResetAllCubeColours();
		submitCube.SetInteractable(false);
	}

	// Loads questions from the assigned text file. Each question is a block separated by a blank line
	void LoadQuestions()
	{
		questions.Clear();

		if (questionsFile != null)
		{
			// Split into question blocks by blank line
			string[] blocks = System.Text.RegularExpressions.Regex.Split(questionsFile.text.Replace("\r\n", "\n"), @"\n\s*\n").Where(block => block.Trim().Length > 0).ToArray();


			// Iterate over each question block (can now have any number of answers)
			foreach (string block in blocks)
			{
				string[] lines = block.Split('\n').Select(line => line.Trim()).Where(line => line.Length > 0).ToArray();

				// Need at least a question line, one option, and a correct answer line
				if (lines.Length < 3) { continue; }

				string questionText = lines[0];
				int optionCount = lines.Length - 2; // everything except the question line and the correct answer line

				string[] options = new string[optionCount];
				for (int i = 0; i < optionCount; i++)
				{
					options[i] = lines[i + 1];
				}

				int correctNumber;
				int.TryParse(lines[lines.Length - 1], out correctNumber);
				int correctIndex = Mathf.Clamp(correctNumber - 1, 0, optionCount - 1);

				questions.Add(new Question(questionText, options, correctIndex));
			}
		}



		// Placeholder 2 question quiz
		if (questions.Count == 0)
		{
			questions.Add(new Question(
				"PLACEHOLDER Q1: which option is correct?", // Question
				new[] { "Placeholder option 1", "Placeholder option 2 (correct)", "Placeholder option 3", "Placeholder option 4" }, // Answers
				1)); // Correct answer

			questions.Add(new Question(
				"PLACEHOLDER Q2: which option is correct?",
				new[] { "Placeholder option 1", "Placeholder option 2", "Placeholder option 3", "Placeholder option 4 (correct)" },
				3));
		}
	}









	// Called by an AnswerCube when it is pulled
	public void OnAnswerCubePulled(AnswerCube cube)
	{

		switch (state)
		{
			case QuizState.NotStarted:
				currentQuestionIndex = 0;
				correctCount = 0;
				state = QuizState.Answering;
				ShowCurrentQuestion();
				return;


			case QuizState.Answering:
				SelectAnswer(cube);
				return;

			case QuizState.Feedback:
				AdvanceToNextQuestion();
				return;

			case QuizState.Finished:
				// Cubes are disabled by this point
				return;
		}
	}








	// Called by the SubmitCube when it is pulled.
	public void OnSubmitCubePulled()
	{
		if (state != QuizState.Answering || selectedAnswerIndex < 0) { return; }
		CheckAnswer();
	}






	void SelectAnswer(AnswerCube cube)
	{
		selectedAnswerIndex = cube.answerIndex;

		for (int i = 0; i < answerCubes.Length; i++)
		{
			if (i == selectedAnswerIndex)
			{
				answerCubes[i].SetGreen();
			}
			else
			{
				answerCubes[i].SetDefault();
			}
		}

		submitCube.SetInteractable(true);
	}







	void CheckAnswer()
	{
		Question current = questions[currentQuestionIndex];
		bool isCorrect = selectedAnswerIndex == current.correctIndex;

		if (isCorrect)
		{
			correctCount++;
		}


		for (int i = 0; i < answerCubes.Length; i++)
		{
			if (i == current.correctIndex)
			{
				answerCubes[i].SetGreen();
			}
			else if (i == selectedAnswerIndex)
			{
				answerCubes[i].SetRed();
			}
			else
			{
				answerCubes[i].SetGrey();
			}
		}

		// Display in green for correct
		string resultText = isCorrect ? "<color=green>Correct answer!</color>" : "<color=red>Incorrect answer</color>";



		instructionMessage.text = resultText + "\nPull any cube to move to the next question.";

		submitCube.SetInteractable(false);
		state = QuizState.Feedback;
	}





	void AdvanceToNextQuestion()
	{
		currentQuestionIndex++;

		if (currentQuestionIndex >= questions.Count)
		{
			FinishQuiz();
			return;
		}

		state = QuizState.Answering;
		ShowCurrentQuestion();
	}







	void ShowCurrentQuestion()
	{
		Question current = questions[currentQuestionIndex];

		System.Text.StringBuilder optionsText = new System.Text.StringBuilder();
		for (int i = 0; i < current.options.Length; i++)
		{
			// Assign letter depending on the index of the answer 
			char optionLetter = (char)('A' + i);
			optionsText.Append(optionLetter).Append(". ").Append(current.options[i]);
			if (i < current.options.Length - 1) { optionsText.Append("\n"); }
		}


		questionText.text = string.Format("{0}\n\n{1}", current.questionText, optionsText.ToString());

		instructionMessage.text = "";
		selectedAnswerIndex = -1;

		ResetAllCubeColours();
		submitCube.SetInteractable(false);
	}






	void FinishQuiz()
	{
		state = QuizState.Finished;

		float percentCorrect = (float)correctCount / (float)questions.Count;
		PlayerPrefs.SetFloat("AssembleCorrect", percentCorrect);

		instructionMessage.text = (percentCorrect == 1f) ? "Well done!" : "";
		questionText.text = string.Format("You scored {0} / {1}", correctCount, questions.Count);



		foreach (AnswerCube cube in answerCubes)
		{
			cube.SetGrey();
			cube.SetInteractable(false);
		}

		submitCube.SetGrey();
		submitCube.SetInteractable(false);
	}







	void ResetAllCubeColours()
	{
		foreach (AnswerCube cube in answerCubes)
		{
			cube.SetDefault();
		}
	}
}