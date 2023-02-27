using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private bool inMotion;
    private bool letChangeTheMotion;
    private float speed;
    public DynamicJoystick dynamicJoystick;
    public Vector3 direction;
    public float _magn;
    public Vector2 _magnVert;
    private Animator anim;
    private bool idle;
    private bool running;
    public bool inTunnel;
    public GameObject tunnel;
    private GameManager gm;
    private Vector3 startRotation;
    public Vector3 startPosition;
    
    void Start()
    {
        startRotation = gameObject.transform.rotation.eulerAngles;
        startPosition = transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        inTunnel = false;
        anim = gameObject.GetComponent<Animator>();
        running = false;
        idle = true;
        inMotion = true;
        letChangeTheMotion = true;
    }

    public bool changeSpeed(float _speed)
    {
        speed = _speed;
        return true;
    }
    public bool changeMotion(bool _change)
    {
        if (letChangeTheMotion)
        {
            inMotion = _change;
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator splineFollow(SplineFollower sf)
    {
        sf.followSpeed = 5f;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (sf.GetPercent() > 0.99d)
            {
                break;
            }
        }
        Debug.Log("EndOfSpline");
        Destroy(sf);
        gameObject.transform.eulerAngles = startRotation;
        gameObject.transform.position = new Vector3(transform.position.x,startPosition.y,transform.position.z);
        changeMotion(true);
    }

    public void inSpline()
    {
        SplineFollower sf = gameObject.GetComponent<SplineFollower>();
        StartCoroutine(splineFollow(sf));
    }
    
    void Update()
    {
        _magnVert = dynamicJoystick.Direction;
        motionOfCharacter();
    }

    
    
    public void animatorController(float s,bool idle=false)
    {
        if (idle)
        {
            anim.speed = 1f;
            anim.SetBool("Idle",true);
            anim.SetBool("Running",false);
            anim.SetBool("Walking",false);   
        }
        else
        {
            if (s > .5f)
            {
                anim.SetBool("Idle",false);
                anim.SetBool("Running",true);   
                anim.SetBool("Walking",false);   
                anim.speed = s;
            }
            else
            {
                anim.SetBool("Idle",false);
                anim.SetBool("Running",false);   
                anim.SetBool("Walking",true);
                anim.speed = 1;
            }
        }
        
    }

    /*private void Answer(GameObject answer)
    {
        Vector3 _target;
        if (answer.GetComponent<InteractiveObjectController>().isTrueAnswer)
        {
            answer.GetComponent<InteractiveObjectController>().answered = true;
            _target = gm.envController.nextLevelStart();
            gm.levelsOfCharacter++;
        }
        else
        {
            _target = gm.envController.currentLevelStart();
        }

        if (_target != null)
        {
            StartCoroutine(Jumping(_target));
        }
        
    }*/

    //Jumping değişecek *-*-*
    private IEnumerator Jumping(Vector3 _target)
    {
        Vector3 start = gameObject.transform.position;
        _target.y = start.y;
        Vector3 mid = (start + _target) / 2;
        mid.y = start.y + 3f;
        float k = 0;
        while (k<1)
        {
            k += Time.deltaTime/2f;
            yield return new WaitForEndOfFrame();
            Vector3 startToMid = Vector3.Lerp(start,mid,k);
            Vector3 midToTarget = Vector3.Lerp(mid,_target,k);
            Vector3 _motion = Vector3.Lerp(startToMid,midToTarget,k);
            gameObject.transform.position = _motion;
        }
        
        
        letChangeTheMotion = true;
        changeMotion(true);
        inTunnel = false;
        gameObject.transform.position = _target;

    }
    
    
    private void motionOfCharacter()
    {
        if (inMotion)
        {
            direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
            _magn = direction.magnitude;
            if (_magn > 0.1f)
            {
                animatorController(_magn);
                gameObject.transform.Translate(Vector3.forward*_magn * Time.deltaTime * speed,Space.Self);
                gameObject.transform.LookAt(gameObject.transform.position + direction);
            }
            else
            {
                animatorController(0,true);
            }
        }
        else
        {
            if (inTunnel)
            {
                if (tunnel != null)
                {
                    Vector3 _direct = (tunnel.transform.position - gameObject.transform.position);
                    distancedis = Mathf.Sqrt((_direct.x * _direct.x) + (_direct.z * _direct.z));
                    if (distancedis > 0.2f)
                    {
                        animatorController(1f);
                        gameObject.transform.Translate( Vector3.forward * Time.deltaTime * speed,Space.Self);
                        Vector3 s = tunnel.transform.position;
                        s.y = gameObject.transform.position.y;
                        gameObject.transform.LookAt(s);
                    }
                    else
                    {
                        animatorController(0,true);
                        if (tunnel.GetComponent<InteractiveObjectController>().isTrueAnswer)
                        {
                            if (!tunnel.GetComponent<InteractiveObjectController>().answered)
                            {
                                inTunnel = false;
                                inMotion = false;
                            }   
                        }
                        else
                        {
                            if (!tunnel.GetComponent<InteractiveObjectController>().answered)
                            {
                                inTunnel = false;
                                inMotion = false;
                            }   
                        }
                    }
                }
                else
                {
                    animatorController(0,true); 
                }
            }
            else
            {
                animatorController(0,true); 
            }

        }
    }
    public float distancedis;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tunnel")
        {
            tunnel = other.gameObject;
            inTunnel = true;
            changeMotion(false);
            letChangeTheMotion = false;
        }
    }

    

    
}
