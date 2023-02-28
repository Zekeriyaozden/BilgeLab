using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    
    void Start()
    {
        TextAsset questdata = Resources.Load<TextAsset>("quesdata");
        string[] data = questdata.text.Split("\n");
        Debug.Log(data.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
