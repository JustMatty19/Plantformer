using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{

    public GameObject PauseMenuController;

    PauseMenuController pauseMenuController;

    void Awake()
    {
        pauseMenuController = PauseMenuController.GetComponent<PauseMenuController>();
    }

    public void resumeGame()
    {
        pauseMenuController.unpause();
    }
}
