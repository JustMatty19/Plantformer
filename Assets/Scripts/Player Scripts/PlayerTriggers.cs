using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTriggers : MonoBehaviour
{
    public GameObject cam;
    public SceneController sceneController;
    public GameObject exitDoorTiles;
    public GameObject checkpointTiles;
    public GameObject cloud;

    PlayerController playerController;
    Rigidbody2D rb2D;
    Animator anim;

    public Tilemap tilemap;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.tag == "Lava")
        {
            if(!PlayerController.lavaImmunity)
            {
                cam.SetActive(false);
                playerController.hasDied();
            }
        }
        
        if(other.tag == "Death")
        {
            cam.SetActive(false);
            playerController.hasDied();
        }

        if(other.tag == "Powerups")
        {
            if(!anim.GetBool("isBurrowing"))
            {
                if(other.name == "Burrow_Powerup")
                {
                    playerController.gainBurrow();
                    Destroy(other.gameObject);
                    cloud.SetActive(true);
                }
                if(other.name == "Fire_Invulnerability_Powerup")
                {
                    playerController.gainLava();
                    Destroy(other.gameObject);
                    exitDoorTiles.SetActive(false);
                }
                if(other.name == "Jump_Powerup")
                {
                    playerController.gainJump();
                    Destroy(other.gameObject);
                    exitDoorTiles.SetActive(false);
                }
                if(other.name == "RoyaltyPowerup")
                {
                    Destroy(other.gameObject);
                    sceneController.nextLevel("EndScene");
                }
            }
        }

        if(other.tag == "Flower")
        {
            if(!anim.GetBool("isBurrowing"))
            {
                playerController.flowerCollector();
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0.5f, 0, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0, 0, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(-0.5f, 0, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0, 0.5f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(-0.5f, 0.5f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0.5f, 1f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0, 1f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(-0.5f, 1f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0.5f, 1.5f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(0, 1.5f, 0)), null);
                tilemap.SetTile(tilemap.WorldToCell(transform.position - new Vector3(-0.5f, 1.5f, 0)), null);
            }
        }

        if(other.tag == "Checkpoint")
        {
            playerController.newCheckpoint(other.gameObject.GetComponent<Checkpoint>().checkpointName);
            playerController.SavePlayer();
        }

        if(other.tag == "NextLevel")
        {
            sceneController.nextLevel(other.gameObject.GetComponent<NextLevel>().levelName);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Hazards")
        {
            playerController.takeDamage();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Checkpoint")
        {
            checkpointTiles.SetActive(false);
        }
    }
}
