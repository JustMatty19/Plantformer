using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    public PauseMenuController pauseMenuController;
    PlayerController playerController;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        anim.SetBool("isLeaving", false);

        if(SceneManager.GetActiveScene().name != "Level-Boss-1" &&
            SceneManager.GetActiveScene().name != "Level-Boss-2" &&
            SceneManager.GetActiveScene().name != "Level-Boss-3")
        {
            setInputEnabled();
        }
    }

    public void setInputEnabled()
    {
        PlayerController.inputEnabled = true;
    }

    public void nextLevel(string level)
    {
        PlayerController.inputEnabled = false;

        if(level == "EndScene")
        {
            playerController.newCheckpoint("Level-1");
            PlayerController.flowerCount = 0;
            PlayerController.lives = 5;
        }

        playerController.SavePlayer();
        Destroy(player);
        StartCoroutine(nextScene(level));
    }

    public void repeatLevel(string level)
    {
        PlayerController.inputEnabled = false;
        if(pauseMenuController.paused)
        {
            pauseMenuController.unpause();
        }
        Time.timeScale = 0;
        StartCoroutine(repeatScene(level));
    }

    IEnumerator nextScene(string level)
    {
        anim.SetBool("isLeaving", true);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(level);
    }

    IEnumerator repeatScene(string level)
    {
        anim.SetBool("isLeaving", true);
        yield return new WaitForSecondsRealtime(1f);
        PlayerController.flowerCount = 0;
        PlayerController.lives = 1;
        playerController.SavePlayer();
        SceneManager.LoadScene(level);
    }
}
