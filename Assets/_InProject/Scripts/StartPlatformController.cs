using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatformController : MonoBehaviour
{
    public GameObject elevator1 , elevator2 , door;
    void Start()
    {
        door.GetComponent<Animator>().enabled = false;
        if (elevator1.GetComponent<ElevatorController>().isComplated &&
            elevator1.GetComponent<ElevatorController>().isComplated)
        {
            door.GetComponent<Animator>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
