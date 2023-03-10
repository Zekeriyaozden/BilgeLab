using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnvironmentController : MonoBehaviour
{
    public GameObject platform1Prefabs;
    public List<GameObject> startPlatforms;
    private float distance;
    public int sizeOfQuestion;
    private int sumOfLevel;
    public GameObject lastPlatformPrefab;
    public GameObject targetPlatform;
    public GameObject targetElev;
    void Start()
    {
        distance = startPlatforms[1].transform.position.z - startPlatforms[0].transform.position.z;
        sizeOfQuestion = GameObject.Find("GameManager").GetComponent<GameManager>().dataReader.sizeOfQuestion();
        int countOfPlatform = sizeOfQuestion / 20;
        Debug.Log(countOfPlatform);
        levelIndex();
        if (countOfPlatform >= 2)
        {
            for (int i = 0; i < countOfPlatform-2; i++)
            {
                GameObject _pltfrm;
                if (countOfPlatform - 3 == i)
                {
                    _pltfrm = Instantiate(lastPlatformPrefab); 
                }
                else
                {
                    _pltfrm = Instantiate(platform1Prefabs);   
                }
                _pltfrm.transform.position = new Vector3(startPlatforms[1].transform.position.x,
                    startPlatforms[1].transform.position.y, startPlatforms[startPlatforms.Count - 1].transform.position.z + 28);
                startPlatforms.Add(_pltfrm);
                Debug.Log(_pltfrm);
                _pltfrm.GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>()
                    .levelIndex = sumOfLevel;
                _pltfrm.GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>().levelIndexer();
                sumOfLevel++;
                _pltfrm.GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>()
                    .levelIndex = sumOfLevel;
                _pltfrm.GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>().levelIndexer();
                sumOfLevel++;
            }
        }
        findFirstUncomplatedElev();
        targetPlatform.GetComponent<StartPlatformController>().setPlatformDests(targetElev);
        startElev();
    }

    public void startElev()
    {
        int _count = startPlatforms.Count;
        for (int i = 0; i < _count; i++)
        {
            if (startPlatforms[i].gameObject.GetComponent<StartPlatformController>().elevator1
                .GetComponent<ElevatorController>().isComplated)
            {
                startPlatforms[i].gameObject.GetComponent<StartPlatformController>().elevator1
                    .GetComponent<ElevatorController>().lockObject.SetActive(false);
            }
            else
            {
                startPlatforms[i].gameObject.GetComponent<StartPlatformController>().elevator1
                    .GetComponent<ElevatorController>().lockObject.SetActive(true);
                startPlatforms[i].gameObject.GetComponent<StartPlatformController>().elevator1
                    .GetComponent<ElevatorController>().sphere.SetActive(true);
                startPlatforms[i].gameObject.GetComponent<StartPlatformController>().elevator1
                    .GetComponent<ElevatorController>().sphere.GetComponent<SphereCollider>().enabled = true;
                
            }
        }
       targetElev.GetComponent<ElevatorController>().lockObject.SetActive(true);
       targetElev.GetComponent<ElevatorController>().sphere.SetActive(false);
       targetElev.GetComponent<ElevatorController>().sphere.GetComponent<SphereCollider>().enabled = false;
    }
    
    public void findFirstUncomplatedElev()
    {
        int _count = startPlatforms.Count;
        bool breakFlag = false;
        for (int i = 0; i < _count; i++)
        {
            if (startPlatforms[i].GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>()
                .isComplated)
            {
                
            }
            else
            {
                targetPlatform = startPlatforms[i];
                targetElev = startPlatforms[i].GetComponent<StartPlatformController>().elevator1;
                breakFlag = true;
                break;
            }

            if (startPlatforms[i].GetComponent<StartPlatformController>().elevator2
                .GetComponent<ElevatorController>().isComplated)
            {
                
            }else
            {
                targetPlatform = startPlatforms[i];
                targetElev = startPlatforms[i].GetComponent<StartPlatformController>().elevator1;
                breakFlag = true;
                break;
            }
        }
        if (!breakFlag)
        {
            targetPlatform = startPlatforms[_count - 1];
            targetElev = startPlatforms[_count - 1].GetComponent<StartPlatformController>().elevator2;
        }
    }
    
    private void levelIndex()
    {
        startPlatforms[0].GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>()
            .levelIndex = 1;
        startPlatforms[0].GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>().levelIndexer();
        startPlatforms[0].GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>()
            .levelIndex = 2;
        startPlatforms[0].GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>().levelIndexer();
        startPlatforms[1].GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>()
            .levelIndex = 3;
        startPlatforms[1].GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>().levelIndexer();
        startPlatforms[1].GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>()
            .levelIndex = 4;
        startPlatforms[1].GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>().levelIndexer();
        sumOfLevel = 5;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
