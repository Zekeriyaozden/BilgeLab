using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private AIManager aiManager;
    private NavMeshAgent navMesh;
    public List<GameObject> dest;
    public GameObject FinalDest;
    public bool isNav;
    public bool TargetForGamePlay;
    public GameObject ElevatorForGamePlay;
    private Vector3 startPos;
    private Transform startParent;
    public float speed;
    private bool isNavmeshed;
    private bool isParachuted;
    private bool isSki;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        startParent = transform.parent;
        startPos = transform.position;
        isNav = true;
        aiManager = GameObject.Find("AIManager").GetComponent<AIManager>();
        dest = aiManager.destList;
        navMesh = gameObject.GetComponent<NavMeshAgent>();
        FinalDest = dest[Random.Range(0, dest.Count)];
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
        
        speed = navMesh.velocity.magnitude;
        
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
            navMesh.destination = FinalDest.transform.position;
            if (Vector3.Distance(gameObject.transform.position, FinalDest.transform.position) < .3f)
            {
                FinalDest = dest[Random.Range(0, dest.Count)];
            }   
        }
    }
}
