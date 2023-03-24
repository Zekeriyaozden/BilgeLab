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
    public List<GameObject> mainChars;
    [HideInInspector]
    public GameObject mainCharacter;
    public CharacterController mainCharacterController;
    public int levelsOfCharacter;
    private GameObject GamePlayEnvs;
    [HideInInspector] public DataReader dataReader;
    public EnvironmentController envController;
    public float splineSpeed;
    public UIManager UIManager;
    public bool isArrowVisible;
    public int finishedAI;
    

    private void Awake()
    {
        finishedAI = 0;
        Debug.Log("playerPrefs" + PlayerPrefs.GetInt("Character",-1));
        if (PlayerPrefs.GetInt("Character", -1) == 0)
        {
            mainCharacter = mainChars[0];
            mainChars[1].SetActive(false);
        }
        else
        {
            mainCharacter = mainChars[1];
            mainChars[0].SetActive(false);
        }
        mainCharacterController = mainCharacter.GetComponent<CharacterController>();
        mainCharacterController.changeSpeed(mainCharacterSpeed);
        dataReader = GameObject.Find("DataManager").GetComponent<DataReader>();
    }
    

    void Start()
    {
        
        levelsOfCharacter = 0;
        if (!isLobby)
        {
            UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
            GamePlayEnvs = GameObject.Find("GamePlayEnvs");
            envController = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
