using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource elevator;
    public AudioSource pipe;
    public AudioSource button;
    public AudioSource truePipe;
    public AudioSource gameMusic;
    public AudioSource loadingScreenMusic;
    public AudioSource parachute;
    public AudioSource win;
    public AudioSource fault;
    public AudioSource walk;
    public bool musicOn;
    public bool soundOn;
    private bool onWalkPaused;
    void Start()
    {
        onWalkPaused = false;
    }

    public void playSound(int _index)
    {
        
        if (_index == 0)
        {
            if (soundOn)
            {
                elevator.Play();
            }
        }
        else if (_index == 1)
        {if (soundOn)
            {
                pipe.Play();    
            }
        }
        else if (_index == 2)
        {
            if (soundOn)
            {
                button.Play();     // 
            }
        }
        else if (_index == 3)
        {
            if (soundOn)
            {
                truePipe.Play();   
            }
        }
        else if (_index == 4)
        {
            if (musicOn)
            {
                gameMusic.Play();   
            }
        }
        else if (_index == 5)
        {
            if (musicOn)
            {
                loadingScreenMusic.Play();   
            }
        }
        else if (_index == 6)
        {
            if (soundOn)
            {
                parachute.Play();   
            }
        }
        else if (_index == 7)
        {
            if (soundOn)
            {
                win.Play();   
            }
        }
        else if (_index == 8)
        {
            if (soundOn)
            {
                fault.Play();   
            }
        }
        else if (_index == 9)
        {
            if (soundOn)
            {
                if (onWalkPaused)
                {
                    walk.UnPause();
                }
                else
                {
                       walk.Play();
                }
            }
        }
    }
    
    public void stopSound(int _index)
    {
        
        if (_index == 0)
        {
            if (soundOn)
            {
                elevator.Stop();
            }
        }
        else if (_index == 1)
        {if (soundOn)
            {
                pipe.Stop();    
            }
        }
        else if (_index == 2)
        {
            if (soundOn)
            {
                button.Stop();      
            }
        }
        else if (_index == 3)
        {
            if (soundOn)
            {
                truePipe.Stop();   
            }
        }
        else if (_index == 4)
        {
            if (musicOn)
            {
                gameMusic.Stop();   
            }
        }
        else if (_index == 5)
        {
            if (musicOn)
            {
                loadingScreenMusic.Stop();   
            }
        }
        else if (_index == 6)
        {
            if (soundOn)
            {
                parachute.Stop();   
            }
        }
        else if (_index == 7)
        {
            if (soundOn)
            {
                win.Stop();   
            }
        }
        else if (_index == 8)
        {
            if (soundOn)
            {
                fault.Stop();   
            }
        }
        else if (_index == 9)
        {
            if (soundOn)
            {
                walk.Pause();   
            }
        }
                
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
