using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int lives;
    public string lastCheckpoint;
    public int flowerCount;
    public bool canBurrow;
    public bool lavaImmunity;
    public bool extraJump;

    public PlayerData ()
    {
        //if(PlayerController.lives != null)
        lives = PlayerController.lives;
        lastCheckpoint = PlayerController.lastCheckpoint;
        flowerCount = PlayerController.flowerCount;
        canBurrow = PlayerController.canBurrow;
        lavaImmunity = PlayerController.lavaImmunity;
        extraJump = PlayerController.extraJump;
    }
}
