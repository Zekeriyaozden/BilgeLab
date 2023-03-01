using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class DataReader : MonoBehaviour
{
    public TextAsset questdata;
    public List<QuestionData> dataSet;
    void Start()
    {
        dataSet = new List<QuestionData>();
        Read();
    }
    void Read()
    {
        string[] Lines = questdata.text.Split("\n");
        //Debug.Log(Lines.Length);
        for (int i=0; i<Lines.Length; i++)
        {
            //Debug.Log(Lines[i]);
            string[] dataLine = Lines[i].Split(",");
            int id, answerSize;
            List<string> answer = new List<string>();
            string question , subject;
            
            Int32.TryParse(dataLine[0],out id);
            if (id == 0)
            {
                continue;
            }
            else
            {
                answer.Add(dataLine[2]);
                answer.Add(dataLine[3]);
                answer.Add(dataLine[4]);
                question = dataLine[1];
                subject = dataLine[7];
                if (dataLine[5] == "")
                {
                    answerSize = 3;
                }else if (dataLine[6] == "")
                {
                    answerSize = 4;
                    answer.Add(dataLine[5]);
                }
                else
                {
                    answerSize = 5;
                    answer.Add(dataLine[5]);
                    answer.Add(dataLine[6]);
                }
                QuestionData dt = new QuestionData(answerSize,question,answer,id,subject);
                dataSet.Add(dt);
            }
            
        }

        for (int i = 0; i < dataSet.Count; i++)
        {
            Debug.Log(dataSet[i].sizeOfAnswer);
        }
    }
}

