using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GameScreenCanvas;
    public GameObject WinCanvas;
    public GameObject LeaderBoardCanvas;
    void Start()
    {
        
    }

    public void winButtonClick()
    {
        SceneManager.LoadScene(0);
    }


    public void sliderControl(float start ,float finish)
    {
        StartCoroutine(_slide(start, finish));
    }

    private IEnumerator _slide(float start, float finish)
    {
        float k = 0;
        float s = math.lerp(start, finish, k);
        while (k < 1)
        {
            yield return new WaitForEndOfFrame();
            k += Time.deltaTime / 3f;
            s = math.lerp(start, finish, k);
            GameScreenCanvas.transform.GetChild(3).gameObject.GetComponent<Slider>().value = s;
        }
    }
    
    void Update()
    {
        
    }
}
