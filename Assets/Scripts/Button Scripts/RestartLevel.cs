using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public SceneController sceneController;

    public void restartLevel()
    {
        sceneController.repeatLevel(PlayerController.lastCheckpoint);
    }
}
