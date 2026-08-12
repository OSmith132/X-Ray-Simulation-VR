using System;

/// <summary>
/// Plain data container for a single quiz question. Not a MonoBehaviour,
/// just holds the parsed text so QuizManager has something to store in a list.
/// </summary>
[Serializable]
public class Question
{
    public string questionText;
    public string[] options;   // Always 4 entries: options[0] to options[3]
    public int correctIndex;   // 0-3, index into options

    public Question(string questionText, string[] options, int correctIndex)
    {
        this.questionText = questionText;
        this.options = options;
        this.correctIndex = correctIndex;
    }
}
