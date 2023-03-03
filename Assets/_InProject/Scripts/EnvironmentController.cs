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
    public List<GameObject> startPlatformPrefs;
    private float distance;
    public GameObject pipeTrue;
    public GameObject pipeFalse;
    private GameManager gm;
    private List<QuestionData> questionAskedList;
    private DataReader dataReader;
    private int sizeOfData;
    public int addedPlatformSize;
    private bool isAdded;
    public int level;

    private void Awake()
    {
        level = PlayerPrefs.GetInt("Level", 1);
        addedPlatformSize = 3;
        /*for (int i = 0; i < 10; i++)
        {
            QuestionData _tempData = dataReader.selectRandomQuestion(1);
            questionAskedList.Add(_tempData);
            PlayerPrefs.SetString(level.ToString() + "-" + i.ToString(),_tempData.id.ToString());
        }*/
        
        /*for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].GetComponent<PlatformController>().questionData = dataReader.selectRandomQuestion(1);
        }*/
    }

    public void currentPlatformControl()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].GetComponent<PlatformController>().questionData = dataReader.selectRandomQuestion(level);
            Debug.Log(platforms[i].GetComponent<PlatformController>().questionData.id);
        }
        List<GameObject> newList = new List<GameObject>();
        for (int i = 0; i < platforms.Count; i++)
        {
            if (i == 0)
            {
               // Debug.Log(platforms[i].GetComponent<PlatformController>().questionData.id + "--------");
            }
            Debug.Log(platforms[i].GetComponent<PlatformController>().questionData.id);
            int _size = platforms[i].GetComponent<PlatformController>().questionData.sizeOfAnswer;
            GameObject _tempObj;
            if (_size == 3)
            {
                if (i == 0)
                {
                    _tempObj = Instantiate(startPlatformPrefs[0]);
                }
                else
                {
                    _tempObj = Instantiate(platformPrefs[0]);
                }
            }else if (_size == 4)
            {
                if (i == 0)
                {
                    _tempObj = Instantiate(startPlatformPrefs[1]);
                }
                else
                {
                    _tempObj = Instantiate(platformPrefs[1]);
                }
            }
            else
            {
                if (i == 0)
                {
                    _tempObj = Instantiate(startPlatformPrefs[2]);
                }
                else
                {
                    _tempObj = Instantiate(platformPrefs[2]);
                }
            }

            _tempObj.GetComponent<PlatformController>().questionData =
                platforms[i].GetComponent<PlatformController>().questionData;
            _tempObj.transform.eulerAngles = platforms[i].transform.eulerAngles;
            _tempObj.transform.position = platforms[i].transform.position;
            newList.Add(_tempObj);
        }

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Destroy");
            Destroy(platforms[i]);
        }
        platforms.Clear();
        for (int i = 0; i < 3; i++)
        {
            platforms.Add(newList[i]);
        }
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        dataReader = gm.dataReader;
        if (!gm.isLobby)
        {
            distance = platforms[1].transform.position.z - platforms[0].transform.position.z;
            platforms[2].transform.position = platforms[1].transform.position + new Vector3(0, 0, distance);
            currentPlatform = platforms[0];
            currentPlatformControl();  
        }
    }
    

    public void addNewPlatform()
    {
        if (addedPlatformSize <= 10)
        {
            addedPlatformSize++;
            int _platformIndex = 0;
            QuestionData questionData = dataReader.selectRandomQuestion(level);
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
    }
    
    void Update()
    {
        
    }
}
