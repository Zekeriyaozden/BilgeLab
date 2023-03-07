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
    private DynamicJoystick dynamicJoystick;
    [HideInInspector]public Vector3 direction;
    [HideInInspector]public float _magn;
    [HideInInspector]public Vector2 _magnVert;
    private Animator anim;
    private bool idle;
    private bool running;
    [HideInInspector]public bool inTunnel;
    [HideInInspector]public GameObject tunnel;
    private GameManager gm;
    private Vector3 startRotation;
    private Vector3 startPosition;
    public GameObject parachute;
    private bool parachuteOn;
    private bool skingOn;
    

    void Start()
    {
        skingOn = parachuteOn = false;
        startRotation = gameObject.transform.rotation.eulerAngles;
        startPosition = transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        inTunnel = false;
        anim = gameObject.GetComponent<Animator>();
        running = false;
        idle = true;
        inMotion = true;
        letChangeTheMotion = true;
        dynamicJoystick = gm.envController.dynamicJoystick;
        
    }

    public void StartBoneElev(GameObject bone)
    {
        gameObject.transform.position = bone.transform.position + Vector3.up * (0.119021f + 0.024498f);
        changeMotion(false);
        GameObject obj = transform.parent.gameObject;
        transform.SetParent(bone.transform);
        StartCoroutine(boneControl(obj));
    }

    private IEnumerator boneControl(GameObject parent)
    {
        yield return new WaitForSeconds(1f);
        transform.SetParent(parent.transform);
        changeMotion(true);
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

    private void onParachute(bool activate)
    {
        if (activate)
        {
            parachuteOn = true;
        }
        else
        {
            parachuteOn = false;
        }
        parachute.gameObject.SetActive(activate);
    }

    private IEnumerator endSplineMotion(Vector3 targetPos,Vector3 targetRot)
    {
        Debug.Log("inThere");
        onParachute(true);
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
        changeMotion(true);
    }
    

    private void endOfSpline(SplineFollower sf)
    {
        Destroy(sf);
        gameObject.transform.eulerAngles = startRotation;
        if (sf.spline.gameObject.transform.parent.gameObject.GetComponent<PipeController>().isTrue)
        {
            gameObject.transform.eulerAngles = startRotation;
            gameObject.transform.position = new Vector3(transform.position.x,startPosition.y,transform.position.z);
            changeMotion(true);
        }
        else
        {
            gameObject.transform.eulerAngles = startRotation;
            gameObject.transform.position = new Vector3(gm.envController.currentPlatform.transform.position.x,transform.position.y,gm.envController.currentPlatform.transform.position.z);
            StartCoroutine(endSplineMotion(
                new Vector3(gm.envController.currentPlatform.transform.position.x, startPosition.y,
                    gm.envController.currentPlatform.transform.position.z - 10f), startRotation));
        }

    }

    private IEnumerator splineFollow(SplineFollower sf)
    {
        skingOn = true;
        sf.followSpeed = gm.splineSpeed;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (sf.GetPercent() > 0.99d)
            {
                break;
            }
        }
        skingOn = false;
        endOfSpline(sf);
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
        if (!parachuteOn)
        {
            anim.SetBool("Parachute",false);
        }
        else
        {
            anim.SetBool("Parachute",true); 
        }

        if (skingOn)
        {
            anim.SetBool("Sking",true);  
        }
        else
        {
            anim.SetBool("Sking",false);
        }
        if (idle)
        {
            {
                anim.speed = 1f;
                anim.SetBool("Idle",true);
                anim.SetBool("Running",false);
                anim.SetBool("Walking",false);     
            }
        }
        else
        {
            if (s > .3f)
            {
                anim.SetBool("Idle",false);
                anim.SetBool("Running",true);
                anim.SetBool("Walking", false);
                if (s < .6f)
                {
                    anim.speed = .6f;
                }
                else
                {
                    anim.speed = s;
                }

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
    //private IEnumerator Jumping(Vector3 _target)
    
    
    
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
    [HideInInspector]public float distancedis;
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
