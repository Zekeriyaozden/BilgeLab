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
    void Start()
    {
        distance = startPlatforms[1].transform.position.z - startPlatforms[0].transform.position.z;
        sizeOfQuestion = GameObject.Find("GameManager").GetComponent<GameManager>().dataReader.sizeOfQuestion();
        int countOfPlatform = sizeOfQuestion / 20;
        if (countOfPlatform > 2)
        {
            for (int i = 0; i < countOfPlatform; i++)
            {
                GameObject _pltfrm = Instantiate(platform1Prefabs);
                _pltfrm.transform.position = new Vector3(startPlatforms[1].transform.position.x,
                    startPlatforms[1].transform.position.y, startPlatforms[1].transform.position.z + distance);
                _pltfrm.GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>()
                    .levelIndex = sumOfLevel;
                sumOfLevel++;
                _pltfrm.GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>()
                    .levelIndex = sumOfLevel;
                sumOfLevel++;
            }
        }
    }

    private void levelIndex()
    {
        startPlatforms[0].GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>()
            .levelIndex = 0;
        startPlatforms[0].GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>()
            .levelIndex = 1;
        startPlatforms[1].GetComponent<StartPlatformController>().elevator1.GetComponent<ElevatorController>()
            .levelIndex = 2;
        startPlatforms[1].GetComponent<StartPlatformController>().elevator2.GetComponent<ElevatorController>()
            .levelIndex = 3;
        sumOfLevel = 4;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
