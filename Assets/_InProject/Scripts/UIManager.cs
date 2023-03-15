using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public GameObject GameScreenCanvas;
    public GameObject truePipe;
    public GameObject falsePipe;
    public GameObject WinCanvas;
    public GameObject LeaderBoardCanvas;
    public GameObject settingUIElement;
    public List<GameObject> stars;
    private Transform targetScreen;
    public GameObject confetiesParent;
    public List<GameObject> leaderBoardAI;
    public List<string> leaderBoardAIName;
    public bool isLobby;
    public GameObject loadingCanvas;
    public GameObject soundOn, soundOff, musicOn, musicOff;
    public bool music, sound;
    
    void Start()
    {
        int _sound = PlayerPrefs.GetInt("Sound", 1);
        int _music = PlayerPrefs.GetInt("Music", 1);
        if (_sound == 1)
        {
            sound = true;
            soundOff.SetActive(false);
            soundOn.SetActive(true);
        }
        else
        {
            sound = false;
            soundOff.SetActive(true);
            soundOn.SetActive(false); 
        }

        if (_music == 1)
        {
            music = true;
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        else
        {
            music = false;
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        
        if (!isLobby)
        {
            int _boardCount = leaderBoardAI.Count;
            for (int i = 0; i < _boardCount; i++)
            {
                leaderBoardAI[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "#" + (i + 2).ToString();
                leaderBoardAI[i].transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "#" + (i + 2).ToString();
                leaderBoardAI[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = leaderBoardAIName[i];
                leaderBoardAI[i].transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = leaderBoardAIName[i];
            }
            targetScreen = GameScreenCanvas.transform.GetChild(0).GetChild(0);
            settingUIElement.SetActive(false);
        }

        Debug.Log("load");
        //loadingCanvas.SetActive(true);
        StartCoroutine(load());
    }

    public void setSound(bool _sound)
    {
        if (_sound)
        {
            PlayerPrefs.SetInt("Sound", 1);
            sound = true;
            soundOff.SetActive(false);
            soundOn.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            sound = false;
            soundOff.SetActive(true);
            soundOn.SetActive(false); 
        }
    }
    
    public void setMusic(bool _music)
    {
        if (_music)
        {
            PlayerPrefs.SetInt("Music", 1);
            music = true;
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            music = false;
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
    }
    

    private IEnumerator load()
    {
        yield return new WaitForSeconds(5f);
        loadingCanvas.SetActive(false);
        GameObject.Find("AIManager").GetComponent<AIManager>().aiSetActive();
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


    public void pipeControl(bool pipe)
    {
        truePipe.SetActive(false);
        falsePipe.SetActive(false);
        if (pipe)
        {
            truePipe.SetActive(true);
        }
        else
        {
            falsePipe.SetActive(true);
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
        SceneManager.LoadScene(1);
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
            GameScreenCanvas.transform.GetChild(1).gameObject.GetComponent<Slider>().value = s;
        }
    }
    
    void Update()
    {
        
    }
}
