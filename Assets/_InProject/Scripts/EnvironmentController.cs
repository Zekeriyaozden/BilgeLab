using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject GamePlayEnvs;
    public List<GameObject> plains;
    public GameObject plainPref0;
    private float distance;
    
    void Start()
    {
        distance = plains[1].transform.position.z - plains[0].transform.position.z;
        GamePlayEnvs = GameObject.Find("GamePlayEnvs");
    }

    public void addNewPlain()
    {
        plains.RemoveAt(0);
        GameObject _inst = Instantiate(plainPref0);
        _inst.transform.position = plains[plains.Count - 1].transform.position + new Vector3(0, 0, distance);
        plains.Add(_inst);
    }
    
    void Update()
    {
        
    }
}
