using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{
    public bool boy,girl;
    public GameObject boyButton,girlButton,startButton;
    public Material mtBoy, mtGirl, mtStart;
    public GameObject comingSpline, goingSpline;
    public GameObject boyObj, girlObj;
    private bool objOnGoing , firstSelect , started;
    //---------------------------------------------
    public GameObject elevator, wing , elevatorBone;
    void Start()
    {
        started = false;
        firstSelect = true;
        objOnGoing = false;
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
        
    }

    private IEnumerator splineFollow(SplineFollower sp , GameObject obj)
    {
        sp.followMode = SplineFollower.FollowMode.Time;
        sp.followDuration = 1f;
        objOnGoing = true;
        obj.GetComponent<Animator>().SetBool("Walking",true);
        obj.GetComponent<Animator>().SetBool("Idle",false);
        bool _control = true;
        while (_control)
        {
            yield return new WaitForEndOfFrame();
            if (sp.GetPercent() > 0.99d)
            {
                _control = false;
            }
        }
        obj.GetComponent<SplineFollower>().enabled = false;
        objOnGoing = false;
        obj.GetComponent<Animator>().SetBool("Walking",false);
        obj.GetComponent<Animator>().SetBool("Idle",true);
    }

    public void boySelected()
    {
        if (objOnGoing || started)
        {
            return;
        }

        if (boy)
        {
            mtGirl.SetFloat("_Trigger",0);
            mtBoy.SetFloat("_Trigger",1.7f);
            boy = true;
            girl = false;
            firstSelect = false;
        }
        else
        {
            mtGirl.SetFloat("_Trigger",0);
            mtBoy.SetFloat("_Trigger",1.7f);
            boy = true;
            girl = false;
            SplineFollower splineComing = boyObj.gameObject.AddComponent<SplineFollower>();
            splineComing.spline = comingSpline.gameObject.GetComponent<SplineComputer>();
            SplineFollower splineGoing = girlObj.gameObject.AddComponent<SplineFollower>();
            splineGoing.spline = goingSpline.gameObject.GetComponent<SplineComputer>();
            StartCoroutine(splineFollow(splineComing , boyObj));
            StartCoroutine(splineFollow(splineGoing , girlObj));
            firstSelect = false;
        }
    }

    public void girlSelected()
    {
        if (objOnGoing || started)
        {
            return;
        }

        if (girl || firstSelect)
        {
            mtBoy.SetFloat("_Trigger",0);
            mtGirl.SetFloat("_Trigger",1.7f);
            girl = true;
            boy = false;
            firstSelect = false;
        }
        else
        {
            mtBoy.SetFloat("_Trigger",0);
            mtGirl.SetFloat("_Trigger",1.7f);
            girl = true;
            boy = false;
            SplineFollower splineComing = girlObj.gameObject.AddComponent<SplineFollower>();
            splineComing.spline = comingSpline.gameObject.GetComponent<SplineComputer>();
            SplineFollower splineGoing = boyObj.gameObject.AddComponent<SplineFollower>();
            splineGoing.spline = goingSpline.gameObject.GetComponent<SplineComputer>();
            StartCoroutine(splineFollow(splineComing , girlObj));
            StartCoroutine(splineFollow(splineGoing , boyObj));
            firstSelect = false;   
        }
    }
    

    private IEnumerator startGameAnim()
    {
        elevator.gameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(2f);
        wing.gameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
    
    
    public void startGame()
    {
        if (started)
        {
            return;
        }
        GameObject _obj = new GameObject();
        if (boy)
        {
            _obj = boyObj; 
            PlayerPrefs.SetInt("Character",0);
        }else if (girl)
        {
            _obj = girlObj;
            PlayerPrefs.SetInt("Character",1);
        }
        else
        {
            Debug.Log("ThereIsAProblem");
            return;
        }
        started = true;
        _obj.transform.SetParent(elevatorBone.transform);

        StartCoroutine(startGameAnim());
    }
    
    void Update()
    {
        if (!boy && !girl)
        {
            startButton.GetComponent<Button>().interactable = false;
            mtStart.SetFloat("_Trigger",0);
        }
        else
        {
            if (objOnGoing)
            {
                startButton.GetComponent<Button>().interactable = false;
                mtStart.SetFloat("_Trigger",0);
            }
            else
            {
                startButton.GetComponent<Button>().interactable = true;
                mtStart.SetFloat("_Trigger",1.7f);
            }
        }
    }
}
