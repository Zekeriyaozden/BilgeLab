using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{
    public bool boy,girl;
    public GameObject boyButton,girlButton,startButton;
    public Material mtBoy, mtGirl, mtStart;
    public GameObject emojiThink, emojiGlass;
    public GameObject boyObj, girlObj;
    void Start()
    {
        int _s = PlayerPrefs.GetInt("Character", -1);
        if (_s != -1)
        {
            SceneManager.LoadScene(1);
        }
        boy = girl = false;
        mtGirl.SetFloat("_Trigger",0);
        mtBoy.SetFloat("_Trigger",0);
        mtStart.SetFloat("_Trigger",0);
    }

    //0 -> boy
    //1 -> girl
    public void characterSelect(int index)
    {
        emojiThink.SetActive(false);
        emojiGlass.SetActive(true);
        if (index == 0)
        {
            boy = true;
            girl = false;
            mtBoy.SetFloat("_Trigger",1.7f);
            mtGirl.SetFloat("_Trigger",0);
            boyObj.GetComponent<Animator>().SetBool("Hand",true);
            girlObj.GetComponent<Animator>().SetBool("Hand",false);
        }else if (index == 1)
        {
            boy = false;
            girl = true;
            mtBoy.SetFloat("_Trigger",0);
            mtGirl.SetFloat("_Trigger",1.7f);
            boyObj.GetComponent<Animator>().SetBool("Hand",false);
            girlObj.GetComponent<Animator>().SetBool("Hand",true);

        }
    }

    public void startGame()
    {
        if (boy)
        {
            PlayerPrefs.SetInt("Character",0);
        }else if (girl)
        {
            PlayerPrefs.SetInt("Character",1);
        }
        else
        {
            Debug.Log("ThereIsAProblem");
            return;
        }
        SceneManager.LoadScene(1);
    }
    
    void Update()
    {
        if (!boy && !girl)
        {
            startButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            startButton.GetComponent<Button>().interactable = true;
            mtStart.SetFloat("_Trigger",1.7f);
        }
    }
}
