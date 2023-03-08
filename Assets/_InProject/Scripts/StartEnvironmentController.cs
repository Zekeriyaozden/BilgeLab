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
