using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnvController : MonoBehaviour
{
    public bool isFirstLevel = false;
    public GameObject GamePlayEnvs;
    private EnvironmentController envController;
    
    void Start()
    {
        triggered = false;
        GamePlayEnvs = GameObject.Find("GamePlayEnvs");
        envController = GamePlayEnvs.GetComponent<EnvironmentController>();
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
            envController.addNewPlain();
            triggered = true;
        }
    }
}
