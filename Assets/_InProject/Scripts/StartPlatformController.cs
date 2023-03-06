using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    private IEnumerator dissolve(Material[] mt)
    {
        float k = 0 , start = -1.1f , target = 1f;
        while (k < 1)
        {
            yield return new WaitForEndOfFrame();
            k += Time.deltaTime / 2f;
            mt[2].SetFloat("_DissolveRing",math.lerp(start,target,k));
            mt[3].SetFloat("_DissolveRing",math.lerp(start,target,k));
            mt[4].SetFloat("_DissolveRing",math.lerp(start,target,k));
            //door.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials = mt;
        }
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
                Material[] mt =  door.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
                yield return new WaitForSeconds(1.2f);
                door.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials = mt;
                StartCoroutine(dissolve(mt));
            }   
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
