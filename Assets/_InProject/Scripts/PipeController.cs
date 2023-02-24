using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    private GameManager gm;
    public bool isTrue;
    public SplineComputer spline;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MainCharacter")
        {
            other.gameObject.GetComponent<CharacterController>().changeMotion(false);
            SplineFollower sp = other.gameObject.AddComponent<SplineFollower>();
            sp.spline = spline;
            other.gameObject.GetComponent<>()
        }
    }
}
