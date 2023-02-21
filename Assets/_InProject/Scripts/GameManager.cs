using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float mainCharacterSpeed;
    //[HideInInspector]
    public GameObject mainCharacter;
    public CharacterController mainCharacterController;
    public int levelsOfCharacter;
    private GameObject GamePlayEnvs;
    private EnvironmentController envController;

    private void Awake()
    {
        mainCharacterController = GameObject.Find("MainCharacter").GetComponent<CharacterController>();
        mainCharacterController.changeSpeed(mainCharacterSpeed);
    }
    

    void Start()
    {
        levelsOfCharacter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
