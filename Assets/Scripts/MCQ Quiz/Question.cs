using System;
using System.Linq;

/// <summary>
/// Plain data container for a single quiz question. Not a MonoBehaviour,
/// just holds the parsed text so QuizManager has something to store in a list.
/// </summary>
[Serializable]
public class Question
{
    public string questionText;
    public string[] options;       // Any number of entries
    private int[] correctIndices;   // One entry = single select question, more than one = multi select question. Also use -1 as a flag for multiselect where there is only one answer

    public Question(string questionText, string[] options, int[] correctIndices)
    {
        this.questionText = questionText;
        this.options = options;
        this.correctIndices = correctIndices;
    }

     public bool IsMultiSelect()
    {
		return (correctIndices.Contains(-1))  || ( correctIndices.Length > 1);
	}


    public int[] GetAnswers()
    {
        return correctIndices.Where(x => x != -1).ToArray(); // Remove all -1 flags before returning

	}
}