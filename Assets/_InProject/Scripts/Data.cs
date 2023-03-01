using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionData
{
    public int id;
    public string question;
    public List<string> answers;
    public int sizeOfAnswer;
    public int subject;
    public List<int> indexesOfTrueAnswer;
    public int selectedLevel = -1;

    public QuestionData(int _sizeOfAnswer,string _question,List<string> _answers,int _id,int _subject,List<int> _indexesOfTrueAnswer)
    {
        this.indexesOfTrueAnswer = _indexesOfTrueAnswer;
        this.sizeOfAnswer = _sizeOfAnswer;
        this.answers = _answers;
        this.id = _id;
        this.question = _question;
        this.subject = _subject;
        Debug.Log("--->>>>");
        for (int i = 0; i < indexesOfTrueAnswer.Count; i++)
        {
            Debug.Log(indexesOfTrueAnswer[i]);
        }
        Debug.Log("<<<<---");
    }
    
    
    
    
    
}

