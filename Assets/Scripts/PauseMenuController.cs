using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public bool paused = false;

    public GameObject PauseButtons;

    void Awake()
    {
        Time.timeScale = 1;
        PauseButtons.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if(!paused)
            {
                pause();
            }
            else if(paused)
            {
                unpause();
            }
        }
    }

    void pause()
    {
        Time.timeScale = 0;
        paused = true;
        PauseButtons.SetActive(true);
    }
    
    public void unpause()
    {
        PauseButtons.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
}
