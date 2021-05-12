using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
        if(data != null)
        {
            PlayerController.lives = data.lives;
            PlayerController.lastCheckpoint = data.lastCheckpoint;
            PlayerController.flowerCount = data.flowerCount;
            PlayerController.canBurrow = data.canBurrow;
            PlayerController.lavaImmunity = data.lavaImmunity;
            PlayerController.extraJump = data.extraJump;

            SceneManager.LoadScene(PlayerController.lastCheckpoint);
        }
    }
}
