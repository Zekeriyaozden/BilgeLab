using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentController : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    public GameObject gamePlayEnvs;
    public List<GameObject> platforms;
    public GameObject currentPlatform;
    public List<GameObject> platformPrefs;
    private float distance;
    public GameObject pipeTrue;
    public GameObject pipeFalse;
    private GameManager gm;
    private List<int> questionAskedList;
    private DataReader dataReader;
    private int sizeOfData;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        dataReader = gm.dataReader;
        distance = platforms[1].transform.position.z - platforms[0].transform.position.z;
        platforms[2].transform.position = platforms[1].transform.position + new Vector3(0, 0, distance);
        currentPlatform = platforms[0];
    }
    

    public void addNewPlatform()
    {
        int _platformIndex = 0;
        QuestionData questionData = dataReader.selectRandomQuestion(4);
        if (questionData.sizeOfAnswer == 3)
        {
            _platformIndex = 0;
        }else if (questionData.sizeOfAnswer == 4)
        {
            _platformIndex = 1;
        }
        else
        {
            _platformIndex = 2;
        }
        GameObject newPlatform = Instantiate(platformPrefs[_platformIndex]);
        newPlatform.GetComponent<PlatformController>().questionData = questionData;
        newPlatform.transform.parent = gamePlayEnvs.transform;
        newPlatform.transform.eulerAngles = platforms[platforms.Count - 1].transform.eulerAngles;
        newPlatform.transform.position = platforms[platforms.Count - 1].transform.position + new Vector3(0,0,distance);
        platforms.Add(newPlatform);
        GameObject willDestroy = platforms[0];
        platforms.RemoveAt(0);
        Destroy(willDestroy);
        Debug.Log("AddedNewPlatform");
    }
    
    void Update()
    {
        
    }
}
