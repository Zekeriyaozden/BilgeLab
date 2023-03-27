using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    public int levelIndex;
    public GameObject elevat;
    public bool isComplated;
    public GameObject wing;
    public GameObject lockObject;
    public List<Transform> destsElev;
    private int indexOfDests;
    public int playerCountInElev;
    private bool isMainInElev;
    private bool isElevMotion;
    public GameObject sphere;
    private List<GameObject> aiList;
    private AIManager aiManager;
    private bool mainCharInElev;
    public int countOfPlayer;
    public GameObject canvasNew;
    public GameObject Unlockparticle;
    public bool openForce;
    public GameObject peopleTick;
    private SoundManager sm;
    private void Awake()
    {
        
        openForce = false;
        Unlockparticle.SetActive(false);
        countOfPlayer = 0;
        aiList = new List<GameObject>();
        isElevMotion = false;
        playerCountInElev = 0;
        indexOfDests = 0;
        isMainInElev = false;
    }

    void Start()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        peopleTick.gameObject.transform.position -= new Vector3(0, 1, 0);
        peopleTick.SetActive(false);
        mainCharInElev = false;
        aiManager = GameObject.Find("AIManager").GetComponent<AIManager>();
        //destsElev.Add(transform.GetChild(transform.childCount - 1).GetChild(0).transform.position);
        lockObject.gameObject.GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().enabled = false;
        wing.gameObject.GetComponent<Animator>().enabled = false;
        elevat = transform.GetChild(0).GetChild(0).gameObject;
        StartCoroutine(unLock());
    }

    /*private IEnumerator lockAnimStart()
    {
        
    }*/
    
    public IEnumerator unLock(bool unlockForce = false)
    {
        yield return new WaitForSeconds(5f);
        if (isComplated)
        {
            peopleTick.SetActive(true);
            canvasNew.SetActive(false);
        }
        if (isComplated || unlockForce)
        {
            lockObject.gameObject.GetComponent<Animator>().SetBool("Unlock",true);
            Unlockparticle.SetActive(true);
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            if (openForce)
            {
                Debug.Log(unlockForce);
                lockObject.gameObject.GetComponent<Animator>().SetBool("Unlock",true);
                Unlockparticle.SetActive(true);
                gameObject.GetComponent<Collider>().isTrigger = true;
            }
            else
            {
                gameObject.GetComponent<Collider>().isTrigger = false;   
            }
        }
    }


    public bool target = false;
    private bool _flag = false;
    private bool afterAIs()
    {
        bool _fr = false;
        if (!_flag) {_fr = true;}
        if (target && !_flag)
        {
            aiManager.destList.RemoveAt(aiManager.destList.Count - 1);
            _flag = true;
        }

        if (_fr)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void levelIndexer()
    {
        if (PlayerPrefs.GetInt(levelIndex.ToString() + "isComplated", 0) == 1)
        {
            isComplated = true;
        }
    }

    private void AIControlForElev(GameObject ai)
    {
        if (aiList.Count >= 8)
        {
            ai.gameObject.GetComponent<AIController>().randomDest();
            return;
        }
        aiList.Add(ai.gameObject);
        ai.gameObject.transform.parent = elevat.transform;
        ai.gameObject.GetComponent<AIController>().isNav = false;
        playerCountInElev++;
        indexOfDests++;
        ai.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        StartCoroutine(elevAI(ai));
    }

    private IEnumerator elevAI(GameObject ai)
    {
        float k = 0;
        Vector3 start = ai.transform.position, target = new Vector3(destsElev[indexOfDests - 1].position.x,ai.transform.position.y,destsElev[indexOfDests-1].position.z);
        while (k < 1)
        {
            yield return new WaitForEndOfFrame();
            k += Time.deltaTime;
            ai.transform.position = Vector3.Lerp(start,target,k);
        }
    }

    // Update is called once per frame
    private string numberOfPerson;
    void Update()
    {
        if (canvasNew)
        {
            canvasNew.transform.LookAt(Camera.main.gameObject.transform);
            canvasNew.transform.localEulerAngles += new Vector3(0, 180, 0);
        }
        if (peopleTick)
        {
            peopleTick.transform.LookAt(Camera.main.gameObject.transform);
            //peopleTick.transform.localEulerAngles += new Vector3(0, 180, 0);
        }
        countOfPlayer = playerCountInElev;
        if (mainCharInElev && playerCountInElev < 8)
        {
            sphere.SetActive(false);
        }
        if ((playerCountInElev > 8 && mainCharInElev) || (playerCountInElev > 7 && !mainCharInElev))
        {
            
            countOfPlayer = playerCountInElev;
            bool _control = afterAIs();
            if (_control)
            {
                aiManager.randomMove();   
            }
            else
            {
                
            }
            sphere.SetActive(true);
            if (!isElevMotion && mainCharInElev)
            {
                StartCoroutine(ElevMotion());
                isElevMotion = true;
            }
        }
        
        if (countOfPlayer > 9)
        {
            countOfPlayer = 9;
        }
        numberOfPerson = countOfPlayer.ToString() + "/9";
        canvasNew.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = numberOfPerson;

    }

    private IEnumerator setElevMot()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().enabled = false;
        wing.gameObject.GetComponent<Animator>().enabled = false;
        for (int i = 0; i < aiList.Count; i++)
        {
            aiList[i].GetComponent<AIController>().reset();
        }
        aiList.Clear();
        wing.gameObject.GetComponent<Animator>().SetFloat("Direct",-1);
        wing.gameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1f);
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("Direct",-1);
        anim.enabled = true;
        sphere.SetActive(false);
    }

    private IEnumerator ElevMotion()
    {
        
        sphere.SetActive(true);
        aiManager.randomMove();
        yield return new WaitForSeconds(1f);
        sm.playSound(0);
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().SetFloat("Direct",1f);
        if (isMainInElev)
        {
            GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(2f);
            wing.gameObject.GetComponent<Animator>().enabled = true;
            wing.gameObject.GetComponent<Animator>().SetFloat("Direct",1f);
            yield return new WaitForSeconds(3f);
            PlayerPrefs.SetInt("Level",levelIndex);
            sm.stopSound(0);
            SceneManager.LoadScene(2);
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(2f);
            wing.gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(1f);
            GetComponent<Collider>().isTrigger = true;
            StartCoroutine(setElevMot());
        }
        playerCountInElev = 0;
        indexOfDests = 0;
        isElevMotion = false;
    }

    public void Call()
    {
        int count = aiManager.AIList.Count;
        for (int i = 0; i < count; i++)
        {
            aiManager.AIList[i].GetComponent<AIController>().Call(elevat.gameObject);
        }
    }

    private void tickOffCounterOn()
    {
        peopleTick.SetActive(false);
        canvasNew.SetActive(true);
    }
    
    

    private IEnumerator elevCenter(GameObject main)
    {
        main.GetComponent<CharacterController>().changeMotion(false);
        Vector3 startPos = main.transform.position;
        Vector3 targetPos = new Vector3(elevat.transform.position.x, startPos.y, elevat.transform.position.z);
        float k = 0;
        while (k < 1)
        {
            yield return new WaitForEndOfFrame();
            k += Time.deltaTime / .3f;
            main.transform.position = Vector3.Lerp(startPos,targetPos,k);
        }
        
        yield return new WaitForSeconds(1f);
        Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tickOffCounterOn();
            playerCountInElev++;
            mainCharInElev = true;
            other.gameObject.transform.parent = elevat.transform;
            StartCoroutine(elevCenter(other.gameObject));
            isMainInElev = true;
            if (playerCountInElev < 9)
            {
                Call();    
            }
            
        }
        else if (other.tag == "AI")
        {
            tickOffCounterOn();
            AIControlForElev(other.gameObject);
        }
    }
}
