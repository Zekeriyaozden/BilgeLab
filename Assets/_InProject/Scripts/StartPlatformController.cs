using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatformController : MonoBehaviour
{
    public GameObject elevator1 , elevator2 , door;
    private bool isOpened;
    void Start()
    {
        isOpened = false;
        StartCoroutine(DoorBeh());
    }

    private IEnumerator DoorBeh()
    {
        yield return new WaitForSeconds(1f);
        if (!isOpened)
        {
            isOpened = true;
            door.GetComponent<Animator>().enabled = false;
            if (elevator1.GetComponent<ElevatorController>().isComplated &&
                elevator2.GetComponent<ElevatorController>().isComplated)
            {
                door.GetComponent<Animator>().enabled = true;
            }   
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
