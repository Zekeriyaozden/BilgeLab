using System;
using System.Collections;
using System.Collections.Generic;
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
    private int playerCountInElev;
    private bool isMainInElev;
    private bool isElevMotion;
    public GameObject sphere;
    private List<GameObject> aiList;
    private AIManager aiManager;
    private void Awake()
    {
        aiList = new List<GameObject>();
        isElevMotion = false;
        playerCountInElev = 0;
        indexOfDests = 0;
        isMainInElev = false;
    }

    void Start()
    {
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
        yield return new WaitForSeconds(.2f);
        if (isComplated || unlockForce)
        {
            Debug.Log(unlockForce);
            lockObject.gameObject.GetComponent<Animator>().SetBool("Unlock",true);
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
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
    void Update()
    {
        if (playerCountInElev >= 5 && !isElevMotion)
        {
            sphere.SetActive(true);
            StartCoroutine(ElevMotion());
            isElevMotion = true;
        }
    }

    private IEnumerator setElevMot()
    {
        yield return new WaitForSeconds(2f);
        //gameObject.GetComponent<Animator>().enabled = false;
        //wing.gameObject.GetComponent<Animator>().enabled = false;
        for (int i = 0; i < aiList.Count; i++)
        {
            aiList[i].GetComponent<AIController>().reset();
        }
        aiList.Clear();
        wing.gameObject.GetComponent<Animator>().SetFloat("Direct",-1);
        yield return new WaitForSeconds(1f);
        Animator anim = gameObject.GetComponent<Animator>();
        //Animation animet = anim
        anim.SetFloat("Direct",-1);
        sphere.SetActive(false);
    }

    private IEnumerator ElevMotion()
    {
        sphere.SetActive(true);
        aiManager.randomMove();
        wing.gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        aiManager.randomMove();
        yield return new WaitForSeconds(1f);
        wing.gameObject.GetComponent<Animator>().SetFloat("Direct",1f);
        gameObject.GetComponent<Animator>().SetFloat("Direct",1f);
        if (isMainInElev)
        {
            GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(2f);
            wing.gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(1f);
            PlayerPrefs.SetInt("Level",levelIndex);
            SceneManager.LoadScene(1);
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
        Debug.Log("Call");
        int count = aiManager.AIList.Count;
        for (int i = 0; i < count; i++)
        {
            aiManager.AIList[i].GetComponent<AIController>().Call(elevat.gameObject);
        }
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
            other.gameObject.transform.parent = elevat.transform;
            StartCoroutine(elevCenter(other.gameObject));
            isMainInElev = true;
            Call();
        }
        else if (other.tag == "AI")
        {
            AIControlForElev(other.gameObject);
        }
    }
}
