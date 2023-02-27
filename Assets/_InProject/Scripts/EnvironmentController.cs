using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentController : MonoBehaviour
{
    public GameObject gamePlayEnvs;
    public List<GameObject> platforms;
    [HideInInspector]public GameObject currentPlatform;
    public List<GameObject> platformPrefs;
    private float distance;
    public GameObject pipeTrue;
    public GameObject pipeFalse;
    
    void Start()
    {
        distance = platforms[1].transform.position.z - platforms[0].transform.position.z;
        platforms[2].transform.position = platforms[1].transform.position + new Vector3(0, 0, distance);
        currentPlatform = platforms[0];
    }
    

    public void addNewPlatform()
    {
        int _platformIndex = Random.Range(0, 3);
        GameObject newPlatform = Instantiate(platformPrefs[_platformIndex]);
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
