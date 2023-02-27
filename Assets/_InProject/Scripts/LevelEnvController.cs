using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnvController : MonoBehaviour
{
    public bool isFirstLevel = false;
    [HideInInspector] public GameObject GamePlayEnvs;
    public List<GameObject> answers;
    public List<GameObject> starts;
    [HideInInspector] public List<int> indexOfTrueAnswers;
    public List<int> indexOfTrueStarts;
    public List<GameObject> trueStartObjects;
    private EnvironmentController envController;
    public Vector3 currentLevelStart;
    
    void Start()
    {
        //currentLevelStart = currentLevelStartObj.transform.position;
        triggered = false;
        GamePlayEnvs = GameObject.Find("GamePlayEnvs");
        envController = GamePlayEnvs.GetComponent<EnvironmentController>();
        for (int i = 0; i < answers.Count; i++)
        {
            if (answers[i].GetComponent<InteractiveObjectController>().isTrueAnswer)
            {
                indexOfTrueAnswers.Add(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (!isFirstLevel && !triggered && other.gameObject.name == "MainCharacter")
        {
            triggered = true;
        }
    }
}
