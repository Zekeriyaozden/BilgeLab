using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformController : MonoBehaviour
{
    public List<TextMeshProUGUI> texts;
    public bool isStart;
    public int answerCount;
    public List<GameObject> answerObjects;
    public List<GameObject> answerHoles;
    public QuestionData questionData;
    private EnvironmentController env;
    private bool isAdded;
    
    void Start()
    {
        isAdded = false;
        env = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentController>();
        _holeAns();
    }
    
    //TO DO
    //yerine otomasyon sistemi gelecek---
    private void _holeAns()
    {
        if (questionData != null)
        {
            texts[0].text = questionData.question;
            for (int i = 0; i < answerHoles.Count; i++)
            {
                texts[i+1].text = questionData.answers[i];
                answerHoles[i].GetComponent<HoleController>().text = texts[i+1];
                for (int j = 0; j < questionData.indexesOfTrueAnswer.Count; j++)
                {
                    if (questionData.indexesOfTrueAnswer[j] == i)
                    {
                        answerHoles[i].GetComponent<HoleController>().isTrue = true;
                    }
                }
            }
        }
        else
        {
            answerHoles[0].GetComponent<HoleController>().isTrue = true;
        }
        locatePipes();
    }
    
    private void locatePipes()
    {
        for (int i = 0; i < answerHoles.Count; i++)
        {
            if (answerHoles[i].GetComponent<HoleController>().isTrue)
            {
                GameObject pipe = Instantiate(env.pipeTrue);
                pipe.transform.position = answerHoles[i].transform.position;
                pipe.transform.parent = transform.GetChild(0);
            }
            else
            {
                GameObject pipe = Instantiate(env.pipeFalse);
                pipe.transform.position = answerHoles[i].transform.position;
                pipe.transform.parent = transform.GetChild(0);
            }
        }
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isAdded)
        {
            if (!isStart)
            {
                env.addNewPlatform();
                isAdded = true;
                env.currentPlatform = gameObject;
            }
        }
    }
}
