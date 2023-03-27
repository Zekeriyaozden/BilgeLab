using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<GameObject> destList;
    public List<GameObject> AIList;
    public int levelOfMainChar;
    private GameManager gm;
    private GameObject bone;
    void Start()
    {
        gm = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        if (!gm.isLobby)
        {
            bone = gm.envController.platforms[0].gameObject.GetComponent<PlatformController>().elevatorBone;
        }
    }

    private IEnumerator aiBeh(GameObject ai)
    {
        Vector3 startPos = ai.transform.position;
        int s = 0;
        bool k = true;
        while (k)
        {
            ai.transform.position = new Vector3(startPos.x ,bone.transform.position.y, startPos.z);
            s++;
            yield return new WaitForEndOfFrame();
            if (s >= 29)
            {
                k = false;
            }
        }
        ai.transform.position = startPos;
    }
    private void AIOnLobby()
    {
        int _cn = AIList.Count;
        for (int i = 0; i < _cn; i++)
        {
            Vector3 t = AIList[i].transform.position;
            
            
        }
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
