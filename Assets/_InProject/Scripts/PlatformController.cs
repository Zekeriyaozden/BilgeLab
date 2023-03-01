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
        int _range = 0;
        for (int i = 0; i < answerHoles.Count; i++)
        {
            int s= Random.Range(0, answerCount);
            if (i == answerHoles.Count - 1)
            {
                _range = answerHoles.Count - 1;
            }
            if (s > answerCount/2)
            {
                _range = i;
            }
        }
        answerHoles[_range].GetComponent<HoleController>().isTrue = true;
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
