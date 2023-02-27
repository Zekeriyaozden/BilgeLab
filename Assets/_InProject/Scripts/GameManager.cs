using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float mainCharacterSpeed;
    public float endOfSplineSpeed;
    //[HideInInspector]
    public GameObject mainCharacter;
    public CharacterController mainCharacterController;
    public int levelsOfCharacter;
    private GameObject GamePlayEnvs;
    public EnvironmentController envController;
    public float splineSpeed;

    private void Awake()
    {
        mainCharacterController = mainCharacter.GetComponent<CharacterController>();
        mainCharacterController.changeSpeed(mainCharacterSpeed);
    }
    

    void Start()
    {
        levelsOfCharacter = 0;
        GamePlayEnvs = GameObject.Find("GamePlayEnvs");
        envController = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
