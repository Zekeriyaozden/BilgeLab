using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionData
{
    public int id;
    public string question;
    public List<string> answers;
    public int sizeOfAnswer;
    public string subject;

    public QuestionData(int _sizeOfAnswer,string _question,List<string> _answers,int _id,string _subject)
    {
        this.sizeOfAnswer = _sizeOfAnswer;
        this.answers = _answers;
        this.id = _id;
        this.question = _question;
        this.subject = _subject;
    }
    
    
    
    
    
}

