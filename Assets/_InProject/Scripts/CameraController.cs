using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameManager gm;
    private GameObject mainCharacter;
    private Vector3 offset;
    [HideInInspector]
    public bool inCameraFollow;
    void Start()
    {
        inCameraFollow = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCharacter = gm.mainCharacterController.gameObject;
        offset = gameObject.transform.position - mainCharacter.transform.position;
    }

    
    
    private void follow()
    {
        if (inCameraFollow)
        {
            gameObject.transform.position = mainCharacter.transform.position + offset;
        }
        else
        {
            return;
        }
    }
    void LateUpdate()
    {
        follow();
    }
}
