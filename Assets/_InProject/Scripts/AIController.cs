using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    private AIManager aiManager;
    private NavMeshAgent navMesh;
    public List<GameObject> dest;
    public List<GameObject> destTrue;
    public List<GameObject> destFalse;
    public GameObject FinalDest;
    public bool isNav;
    public bool TargetForGamePlay;
    public GameObject ElevatorForGamePlay;
    private Vector3 startPos;
    private Vector3 startRot;
    private Transform startParent;
    public float speed;
    private bool isNavmeshed;
    private bool isParachuted;
    private bool isSki;
    private Animator anim;
    public int currentLevel;
    private GameManager gm;
    private bool isLobby;
    private int select;
    private bool isSpline;
    public GameObject currentPlatform;
    public GameObject parachute;
    private bool zeroPlat;
    private Vector3 v3Target;
    void Start()
    {
        currentLevel = -1;
        zeroPlat = false;
        startRot = transform.eulerAngles;
        isSpline = false;
        destTrue = new List<GameObject>();
        destFalse = new List<GameObject>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        isLobby = gm.isLobby;
        anim = GetComponent<Animator>();
        startParent = transform.parent;
        startPos = transform.position;
        isNav = true;
        aiManager = GameObject.Find("AIManager").GetComponent<AIManager>();

        navMesh = gameObject.GetComponent<NavMeshAgent>();

        if (!gm.isLobby)
        {
            StartCoroutine(setV3());
            v3Target = new Vector3(0, -.3f, 0);
            Debug.Log("isnav");
            isNav = false;
            dest = new List<GameObject>();
            StartCoroutine(waitSecond());
        }
        else
        {
            gameObject.SetActive(false);
            dest = aiManager.destList;
            FinalDest = dest[Random.Range(0, dest.Count)];
        }
        
    }

    private IEnumerator setV3()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.position += v3Target;
    }
    
    private IEnumerator waitSecond()
    {
        yield return new WaitForSeconds(.2f);
        currentPlatform = gm.envController.platforms[0];
    }
    
    public void setActive()
    {
        gameObject.SetActive(true);
    }

    public void onParachute(bool _parac)
    {
        if (_parac)
        {
            parachute.SetActive(true);
            gameObject.GetComponent<Animator>().SetBool("Parachute",true);
            isParachuted = true;
        }
        else
        {
            isParachuted = false;
            parachute.SetActive(false);
            gameObject.GetComponent<Animator>().SetBool("Parachute",false);
        }
    }
    
    public void inSpline()
    {
        isSpline = true;
        isNav = false;
        navMesh.enabled = false;
        SplineFollower sf = gameObject.GetComponent<SplineFollower>();
        StartCoroutine(splineListener(sf));
    }
    
    private IEnumerator splineListener(SplineFollower sf)
    {
        bool _flag = true;
        while (_flag)
        {
            yield return new WaitForSeconds(.1f);
            if (sf.GetPercent() > 0.99f)
            {
                _flag = false;
            }
        }
        endOfSpline(sf);
    }
    
    private void endOfSpline(SplineFollower sf)
    {
        gameObject.transform.eulerAngles = startRot;
        if (sf.spline.gameObject.transform.parent.gameObject.GetComponent<PipeController>().isTrue)
        {
            Destroy(sf);
            navMesh.enabled = true;
            isNav = true;
            isSpline = false;
        }
        else
        {
            onParachute(true);
            Destroy(sf);
            gameObject.transform.eulerAngles = startRot;
            
            gameObject.transform.position = new Vector3(currentPlatform.transform.position.x,transform.position.y,currentPlatform.transform.position.z);
            StartCoroutine(endSplineMotion(
                new Vector3(currentPlatform.transform.position.x, startPos.y,
                    currentPlatform.transform.position.z - 10f), startRot));
        }

    }
    
    private IEnumerator endSplineMotion(Vector3 targetPos,Vector3 targetRot)
    {
        //onParachute(true);
        float k = 0;
        Vector3 startRot = gameObject.transform.eulerAngles;
        Vector3 startPos = gameObject.transform.position;
        while (k <= 1)
        {
            yield return new WaitForEndOfFrame();
            gameObject.transform.position = Vector3.Lerp(startPos,targetPos,k);
            k += Time.deltaTime * gm.endOfSplineSpeed / 2f;
        }
        onParachute(false);
        navMesh.enabled = true;
        isNav = true;
        isSpline = false;
        //onParachute(false);
        //changeMotion(true);
    }
    
    public void randomDest()
    {
        FinalDest = dest[Random.Range(0, dest.Count)];
    }
    public void Call(GameObject tempDest)
    {
        Debug.Log(tempDest);
        FinalDest = tempDest;
    }

    public void reset()
    {
        transform.parent = startParent;
        transform.position = startPos;
        navMesh.enabled = true;
        isNav = true;
        FinalDest = dest[Random.Range(0, dest.Count)];
    }
    
    
    public void animControl()
    {
        if (navMesh)
        {
            speed = navMesh.velocity.magnitude;   
        }

        if (navMesh.enabled)
        {
            isNavmeshed = true;
        }
        else
        {
            isNavmeshed = false;
        }
        if (!isNavmeshed)
        {
            if (isParachuted)
            {
                anim.SetBool("Idle" , false);
                anim.SetBool("Walk" , false);
                anim.SetBool("Run" , false);
            }else if (isSki)
            {
                
            }
            else
            {
                anim.SetBool("Idle" , true);
                anim.SetBool("Walk" , false);
                anim.SetBool("Run" , false);
            }
        }
        else
        {
            if (speed == 0)
            {
                anim.SetBool("Idle" , true);
                anim.SetBool("Walk" , false);
                anim.SetBool("Run" , false);
            }else if (speed < 1f)
            {
                anim.SetBool("Idle" , false);
                anim.SetBool("Walk" , true);
                anim.SetBool("Run" , false);
            }
            else
            {
                anim.SetBool("Idle" , false);
                anim.SetBool("Walk" , false);
                anim.SetBool("Run" , true);
            }
        }
    }
    
    void Update()
    {

        animControl();
        if (isNav)
        {
            if (FinalDest == null)
            {
                randomDest();
                return;
            }
            navMesh.destination = FinalDest.transform.position;   
            
            if (isLobby && Vector3.Distance(gameObject.transform.position, FinalDest.transform.position) < .8f)
            {
                FinalDest = dest[Random.Range(0, dest.Count)];
            }
            else if (!isLobby && Vector3.Distance(gameObject.transform.position, FinalDest.transform.position) < 1f)
            {
               select = Random.Range(0, 10);
               if (aiManager.levelOfMainChar < currentLevel)
               {
                   select = 10;
               }
               else if (aiManager.levelOfMainChar == currentLevel+1)
               {
                   select = Random.Range(0, 10);
               }else if (aiManager.levelOfMainChar > currentLevel + 1)
               {
                   select = 0;
               }
               if (select < 3)
               {
                   FinalDest = destTrue[Random.Range(0, destTrue.Count)];
               }else if (select < 5 && select > 1)
               {
                   if (destFalse.Contains(FinalDest))
                   {
                       FinalDest = dest[Random.Range(0, dest.Count)];   
                   }
                   else
                   {
                       FinalDest = destFalse[Random.Range(0, destFalse.Count)];   
                   }
               }
               else
               {
                   FinalDest = dest[Random.Range(0, dest.Count)];   
               }
            }
        }
    }

    private IEnumerator destsForTrue(PlatformController pc)
    {
        bool _flag = true;
        while (_flag)
        {
            yield return new WaitForSeconds(.1f);
            if (pc.isHoled)
            {
                _flag = false;
            }
        }
        int _count = pc.answerHoles.Count;
        for (int i = 0; i < _count; i++)
        {
            if (pc.answerHoles[i].GetComponent<HoleController>().isTrue)
            {
                destTrue.Add(pc.answerHoles[i]);
            }
            else
            {
                destFalse.Add(pc.answerHoles[i]);
            }
        }
        FinalDest = dest[Random.Range(0, dest.Count)];
        if (!isSpline)
        {
            isNav = true;
        }
    }
    private void waitForPlatform(GameObject other)
    {
        dest.Clear();
        int _count = other.gameObject.GetComponent<PlatformController>().dests.Count;
        for (int i = 0; i < _count; i++)
        {
            dest.Add(other.gameObject.GetComponent<PlatformController>().dests[i]);
        }
        destFalse.Clear();
        destTrue.Clear();
        StartCoroutine(destsForTrue(other.gameObject.GetComponent<PlatformController>()));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            currentPlatform = other.gameObject;
            if (other.gameObject.GetComponent<PlatformController>().level == currentLevel)
            {
                int _count = other.gameObject.GetComponent<PlatformController>().dests.Count;
                for (int i = 0; i < _count; i++)
                {
                    dest.Add(other.gameObject.GetComponent<PlatformController>().dests[i]);
                }
                FinalDest = dest[Random.Range(0, dest.Count)];
            }
            else
            {
                currentLevel = other.gameObject.GetComponent<PlatformController>().level;
                if (currentLevel > 8)
                {
                    gm.finishedAI++;
                }
                waitForPlatform(other.gameObject);
            }
        }
    }
}
