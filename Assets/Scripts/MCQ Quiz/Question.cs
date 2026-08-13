using System;

/// <summary>
/// Plain data container for a single quiz question. Not a MonoBehaviour,
/// just holds the parsed text so QuizManager has something to store in a list.
/// </summary>
[Serializable]
public class Question
{
    public string questionText;
    public string[] options;       // Any number of entries
    public int[] correctIndices;   // One entry = single select question, more than one = multi select question

    public Question(string questionText, string[] options, int[] correctIndices)
    {
        this.questionText = questionText;
        this.options = options;
        this.correctIndices = correctIndices;
    }
}