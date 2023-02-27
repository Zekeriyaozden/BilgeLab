using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    private bool isUsed;
    private GameManager gm;
    public bool isTrue;
    public SplineComputer spline;
    void Start()
    {
        isUsed = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isUsed)
        {
           CharacterController _controller = other.gameObject.GetComponent<CharacterController>();
            SplineFollower sp = other.gameObject.AddComponent<SplineFollower>();
            sp.motion.rotationOffset = new Vector3(-90f,0,0);
            _controller.changeMotion(false);
            sp.spline = spline;
            _controller.inSpline();
            if (isTrue)
            {
                gameObject.transform.parent = null;
            }
            //isUsed = true;
        }
    }
}
