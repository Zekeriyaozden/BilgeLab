using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameManager gm;
    private GameObject mainCharacter;
    [HideInInspector]public Vector3 offset;
    //[HideInInspector]
    public bool inCameraFollow;
    void Start()
    {
        inCameraFollow = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCharacter = gm.mainCharacterController.gameObject;
        offset = gameObject.transform.position - mainCharacter.transform.position;
    }


    public void endGame()
    {
        inCameraFollow = false;
        StartCoroutine(cameraMotionEnd(mainCharacter.transform.position));
    }

    private IEnumerator cameraMotionEnd(Vector3 charPos)
    {
        float k = 0;
        Vector3 startPos = gameObject.transform.position;
        while (k < .4f)
        {
            k += Time.deltaTime / 2f;
            transform.position = Vector3.Lerp(startPos, charPos, k);
            yield return new WaitForEndOfFrame();
        }
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
