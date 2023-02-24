using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject GamePlayEnvs;
    public List<GameObject> plains;
    [HideInInspector]public GameObject currentPlain;
    public GameObject plainPref3;
    public GameObject plainPref4;
    public GameObject plainPref5;
    private float distance;
    
    void Start()
    {
        currentPlain = plains[0];
        distance = plains[1].transform.position.z - plains[0].transform.position.z;
        GamePlayEnvs = GameObject.Find("GamePlayEnvs");
    }

    public bool drawSpline(Vector3 _start,Vector3 _target,bool trueAnswer = true)
    {
        try
        {
            if (trueAnswer)
            {
                //Debug.Log("drawing spline to target");
            }
            else
            {
                //Debug.Log("drawing spline to themself");
            }
                
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            return false;
        }
    }
    
    

    public void addNewPlain()
    {
        GameObject willDestroy = plains[0];
        plains.RemoveAt(0);
        Destroy(willDestroy);
        //GameObject _inst = Instantiate(plainPref0);
        //_inst.transform.position = plains[plains.Count - 1].transform.position + new Vector3(0, 0, distance);
        //plains.Add(_inst);
    }
    
    void Update()
    {
        
    }
}
