using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float mainCharacterSpeed;
    public float endOfSplineSpeed;
    public bool isLobby = false;
    //[HideInInspector]
    public GameObject mainCharacter;
    public CharacterController mainCharacterController;
    public int levelsOfCharacter;
    private GameObject GamePlayEnvs;
    [HideInInspector] public DataReader dataReader;
    public EnvironmentController envController;
    public float splineSpeed;

    private void Awake()
    {
        mainCharacterController = mainCharacter.GetComponent<CharacterController>();
        mainCharacterController.changeSpeed(mainCharacterSpeed);
        dataReader = GameObject.Find("DataManager").GetComponent<DataReader>();
    }
    

    void Start()
    {
        levelsOfCharacter = 0;
        if (!isLobby)
        {
            GamePlayEnvs = GameObject.Find("GamePlayEnvs");
            envController = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
