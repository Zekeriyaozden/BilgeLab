using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectController : MonoBehaviour
{
    public bool isAnswer;
    public bool isTrueAnswer;
    public bool answered;
    void Start()
    {
        answered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
