using System.Collections;
using System.Collections.Generic;
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
    
    void Start()
    {
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
        }
        else
        {
            anim.SetBool("Idle",false);
            anim.SetBool("Running",true);
            anim.speed = s;
        }
        
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
            animatorController(0,true);
        }
    }
    
}
