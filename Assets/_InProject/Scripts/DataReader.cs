using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class DataReader : MonoBehaviour
{
    public TextAsset questdata;
    public List<QuestionData> dataSet;

    private void Awake()
    {
        dataSet = new List<QuestionData>();
        Read();
    }

    public int sizeOfQuestion()
    {
        //Debug.Log(dataSet.Count);
        return dataSet.Count;
    }
    
    public QuestionData selectRandomQuestion(int Level)
    {

        List<int> selectebleIndexes = new List<int>();
        
        for (int i = 0; i < dataSet.Count; i++)
        {
            if ((PlayerPrefs.GetInt(i.ToString(), -1) == Level))
            {
                selectebleIndexes.Add(i);   
            }
        }

        if (selectebleIndexes.Count < 10)
        {
            int _counter = selectebleIndexes.Count;
            for (int i = 0; i < dataSet.Count; i++)
            {
                if ((PlayerPrefs.GetInt(i.ToString(), -1) == -1))
                {
                    selectebleIndexes.Add(i);
                    PlayerPrefs.SetInt(i.ToString(),Level);
                    _counter++;
                    if (_counter >= 10)
                    {
                        break;
                    }
                }
            }
        }

        if (selectebleIndexes.Count < 1)
        {
            return null;
        }
        else
        {
            int randIndex = Random.Range(0, selectebleIndexes.Count-1);
            int randId = selectebleIndexes[randIndex];
            QuestionData dt = dataSet.Find(x => x.id == randId);
            return dt;
        }
    }
    
    

    void Read()
    {
        string[] Lines = questdata.text.Split("\n");
        for (int i = 0; i < Lines.Length; i++)
        {
            Debug.Log(Lines[i]);
        }
        //Debug.Log(Lines.Length);
        for (int i=0; i<Lines.Length; i++)
        {
            //Debug.Log(Lines[i]);
            string[] dataLine = Lines[i].Split(";");
            int id, answerSize, subject;
            List<string> answer = new List<string>();
            string question;
            List<int> indexesOfTrueAns = new List<int>();
            
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
                subject = 1;
                if (subject != 0)
                {
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
                    string trueAns0 = dataLine[7];
                    string trueAns1;
                    string trueAns2;
                    try
                    {
                        trueAns1 = dataLine[8];
                    }
                    catch (Exception e)
                    {
                        trueAns1 = "";
                        Console.WriteLine(e);
                        throw;
                    }
                    try
                    {
                        trueAns2 = dataLine[9];
                    }
                    catch (Exception e)
                    {
                        trueAns2 = "";
                        Console.WriteLine(e);
                        throw;
                    }

                    if (trueAns0 != "")
                    {
                        if (trueAns0 == "a" || trueAns0 == "A")
                        {
                            indexesOfTrueAns.Add(0);
                        }else if (trueAns0 == "b" || trueAns0 == "B")
                        {
                            indexesOfTrueAns.Add(1);
                        }else if (trueAns0 == "c" || trueAns0 == "C")
                        {
                            indexesOfTrueAns.Add(2);
                        }else if (trueAns0 == "d" || trueAns0 == "D")
                        {
                            indexesOfTrueAns.Add(3);
                        }else if (trueAns0 == "e" || trueAns0 == "E")
                        {
                            indexesOfTrueAns.Add(4);
                        }
                    }
                    if (trueAns1 != "")
                    {
                        if (trueAns1 == "a" || trueAns1 == "A")
                        {
                            indexesOfTrueAns.Add(0);
                        }else if (trueAns1 == "b" || trueAns1 == "B")
                        {
                            indexesOfTrueAns.Add(1);
                        }else if (trueAns1 == "c" || trueAns1 == "C")
                        {
                            indexesOfTrueAns.Add(2);
                        }else if (trueAns1 == "d" || trueAns1 == "D")
                        {
                            indexesOfTrueAns.Add(3);
                        }else if (trueAns1 == "e" || trueAns1 == "E")
                        {
                            indexesOfTrueAns.Add(4);
                        }
                    }
                    if (trueAns2 != "")
                    {
                        if (trueAns2 == "a" || trueAns2 == "A")
                        {
                            indexesOfTrueAns.Add(0);
                        }else if (trueAns2 == "b" || trueAns2 == "B")
                        {
                            indexesOfTrueAns.Add(1);
                        }else if (trueAns2 == "c" || trueAns2 == "C")
                        {
                            indexesOfTrueAns.Add(2);
                        }else if (trueAns2 == "d" || trueAns2 == "D")
                        {
                            indexesOfTrueAns.Add(3);
                        }else if (trueAns2 == "e" || trueAns2 == "E")
                        {
                            indexesOfTrueAns.Add(4);
                        }
                    }

                    QuestionData dt = new QuestionData(answerSize,question,answer,id,subject,indexesOfTrueAns);
                    dataSet.Add(dt);
                }
            }
        }

    }
    
    
}

