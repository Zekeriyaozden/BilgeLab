using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    public int levelIndex;
    public GameObject elevat;
    public bool isComplated;


    private void Awake()
    {

    }

    void Start()
    {
        GetComponent<Animator>().enabled = false;
        elevat = transform.GetChild(0).GetChild(0).gameObject;
    }

    public void levelIndexer()
    {
        if (PlayerPrefs.GetInt(levelIndex.ToString() + "isComplated", 0) == 1)
        {
            isComplated = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    private IEnumerator elevCenter(GameObject main)
    {
        main.GetComponent<CharacterController>().changeMotion(false);
        Vector3 startPos = main.transform.position;
        Vector3 targetPos = new Vector3(elevat.transform.position.x, startPos.y, elevat.transform.position.z);
        float k = 0;
        while (k < 1)
        {
            yield return new WaitForEndOfFrame();
            k += Time.deltaTime / .3f;
            main.transform.position = Vector3.Lerp(startPos,targetPos,k);
        }
        yield return new WaitForSeconds(1f);
        Debug.Log("sds");
        Camera.main.gameObject.GetComponent<CameraController>().enabled = false;
        GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("Level",levelIndex);
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.parent = elevat.transform;
            StartCoroutine(elevCenter(other.gameObject));
        }
    }
}
