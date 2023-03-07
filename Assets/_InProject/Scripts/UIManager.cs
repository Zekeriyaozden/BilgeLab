using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public GameObject GameScreenCanvas;
    public GameObject WinCanvas;
    public GameObject LeaderBoardCanvas;
    public GameObject settingUIElement;
    public List<GameObject> stars;
    private Transform targetScreen;
    public GameObject confetiesParent;
    void Start()
    {
        targetScreen = GameScreenCanvas.transform.GetChild(0).GetChild(0);
        settingUIElement.SetActive(false);
    }
    private bool isSetingOpen = false;
    public void settingUI(bool set)
    {
        if (set)
        {
            isSetingOpen = true;
            settingUIElement.SetActive(true);
        }
        else
        {
            isSetingOpen = false;
            settingUIElement.SetActive(false);
        }
    }

    public void starsStart()
    {
        for (int i = 0; i < stars.Count; i++)
        {
            StartCoroutine(starCor(stars[i]));
        }
    }

    private IEnumerator starCor(GameObject star)
    {
        Vector3 startTrans = star.gameObject.transform.position;
        float sec0,sec1,k = 0;
        sec1 = Random.Range(0, .4f);
        sec0 = Random.Range(.6f, 1f);
        yield return new WaitForSeconds(sec0);
        star.SetActive(true);
        yield return new WaitForSeconds(sec1);
        while (k < 1)
        {
            yield return new WaitForEndOfFrame();
            k += Time.deltaTime;
            star.transform.position = Vector3.Lerp(startTrans, targetScreen.position, k);
        }
       // star.SetActive(false);
    }

    public void winButtonClick()
    {
        SceneManager.LoadScene(0);
    }


    public void winConfety()
    {
        int count = confetiesParent.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            confetiesParent.transform.GetChild(i).gameObject.SetActive(true);
        }
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
