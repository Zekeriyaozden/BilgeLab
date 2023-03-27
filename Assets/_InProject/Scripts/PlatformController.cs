using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlatformController : MonoBehaviour
{
    public List<TextMeshProUGUI> texts;
    public bool isStart;
    public int answerCount;
    //public List<GameObject> answerObjects;
    public List<GameObject> answerHoles;
    public QuestionData questionData;
    private EnvironmentController env;
    private bool isAdded;
    public bool isLast = false;
    public GameObject elevatorBone;
    public List<GameObject> dests;
    public bool isHoled;
    public int level;
    public List<GameObject> arrows;
    private List<GameObject> trueArrows;
    private SoundManager sm;
    public bool isFault;
    void Start()
    {
        isFault = false;
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        trueArrows = new List<GameObject>();
        isHoled = false;
        isAdded = false;
        env = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentController>();
        if (!isLast)
        {
            _holeAns();
            isHoled = true;
        }
    }
    
    
    
    
    //TO DO
    private void _holeAns()
    {
        if (questionData != null)
        {
            texts[0].text = questionData.question;
            for (int i = 0; i < answerHoles.Count; i++)
            {
                texts[i+1].text = questionData.answers[i];
                answerHoles[i].GetComponent<HoleController>().text = texts[i+1];
                for (int j = 0; j < questionData.indexesOfTrueAnswer.Count; j++)
                {
                    if (questionData.indexesOfTrueAnswer[j] == i)
                    {
                        answerHoles[i].GetComponent<HoleController>().isTrue = true;
                        trueArrows.Add(arrows[i]);
                    }
                }
            }
        }
        else
        {
            answerHoles[0].GetComponent<HoleController>().isTrue = true;
        }
        locatePipes();
    }
    
    private void locatePipes()
    {
        for (int i = 0; i < answerHoles.Count; i++)
        {
            if (answerHoles[i].GetComponent<HoleController>().isTrue)
            {
                GameObject pipe = Instantiate(env.pipeTrue);
                pipe.GetComponent<PipeController>().platform = gameObject;
                pipe.transform.position = answerHoles[i].transform.position;
                pipe.transform.parent = transform.GetChild(0);
            }
            else
            {
                GameObject pipe = Instantiate(env.pipeFalse);
                pipe.GetComponent<PipeController>().platform = gameObject;
                pipe.transform.position = answerHoles[i].transform.position;
                pipe.transform.parent = transform.GetChild(0);
            }
        }
    }
    
    void Update()
    {
        
    }

    private IEnumerator winUI()
    {
        env.gm.mainCharacterController.gameEnd = true;
        env.gm.UIManager.winConfety();
        yield return new WaitForSeconds(1f);
        
        env.gm.UIManager.WinCanvas.SetActive(true);
        env.gm.UIManager.starsStart();
    }

    
    
    private IEnumerator LeaderBoard(GameObject mainChar)
    {
        yield return new WaitForSeconds(.5f);
        if (GameObject.Find("AIManager").GetComponent<AIManager>().levelOfMainChar < 8)
        {
            
        }
        else
        {
            sm.playSound(7);
            mainChar.gameObject.GetComponent<CharacterController>().changeMotion(false);
            Camera.main.gameObject.GetComponent<CameraController>().endGame();
            PlayerPrefs.SetInt(env.level.ToString() + "isComplated" , 1);
            env.gm.UIManager.setLeaderBoard();
            env.gm.UIManager.LeaderBoardCanvas.SetActive(true);
            //env.gm.UIManager.LeaderBoardCanvas.GetComponent<Animator>().speed = .2f;
            StartCoroutine(winUI());   
        }
    }

    public void onArrows()
    {
        if (!env.gm.isArrowVisible)
        {
            return;
        }
        int _count = trueArrows.Count;
        for (int i = 0; i < _count; i++)
        {
            trueArrows[i].SetActive(true);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isAdded && !isLast)
        {
            if (!isStart)
            {
                env.addNewPlatform();
                isAdded = true;
                env.currentPlatform = gameObject;
            }

            if (!isStart && env.platforms[env.platforms.Count - 1] == gameObject)
            {
                //PlayerPrefs
 
            }
        }

        if (isLast)
        {
            StartCoroutine(LeaderBoard(other.gameObject));
        }
    }
}
