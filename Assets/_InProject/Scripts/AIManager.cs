using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<GameObject> destList;
    public List<GameObject> AIList;
    public int levelOfMainChar;
    void Start()
    {
        //Değişecek

    }
    
    public void aiSetActive()
    {
        int _count = AIList.Count;
        for (int i = 0; i < _count; i++)
        {
            AIList[i].gameObject.GetComponent<AIController>().setActive();
        }
    }
    
    public void setDest(List<GameObject> dests)
    {
        Debug.Log("destListss");
        destList = dests;
        int _count = AIList.Count;
        for (int i = 0; i < _count; i++)
        {
            AIList[i].GetComponent<AIController>().dest = destList;
        }
    }

    public void randomMove()
    {
        int _count = AIList.Count;
        for (int i = 0; i < _count; i++)
        {
            AIList[i].GetComponent<AIController>().randomDest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
