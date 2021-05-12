using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    public SceneController sceneController;
    public PauseMenuController pauseMenuController;

    BoxCollider2D bc2D;
    Animator anim;
    PlayerMovement playerMovement;
    PlayerJumping playerJumping;

    public GameObject head;
    public GameObject body;
    public GameObject burrow;

    [SerializeField]
    float invincibilityDurationSeconds = 1.5f;

    [SerializeField]
    float invincibilityDeltaTime = 0.15f;

    public static int lives = 5;

    public static float speed = 4f;
    public static float jump = 7f;
    public static float fallMultiplier = 3f;
    public static float lowJumpMultiplier = 2.5f;

    public static int flowerCount = 0;

    public static string lastCheckpoint;

    public static bool canBurrow = false;
    public static bool lavaImmunity = false;
    public static bool extraJump = false;

    public static bool timeSlowed = false;

    public static bool isInvincible;

    public static bool inputEnabled;

    void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJumping = GetComponent<PlayerJumping>();
    }

    void Start()
    {
        isInvincible = false;
        
        if(SceneManager.GetActiveScene().name != "Level-1")
        {
            LoadPlayer();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            lavaImmunity = !lavaImmunity;
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            extraJump = !extraJump;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            canBurrow = !canBurrow;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(playerMovement.notGrounded() == 0 && canBurrow)
            {
                bc2D.size = new Vector2(0.6898985f, 0.2f);
                bc2D.offset = new Vector2(-0.0001852f, -0.3902f);
                anim.SetBool("isBurrowing", true);
                head.SetActive(false);
                body.SetActive(false);
                speed = 2.5f;
            }
        }
        if(anim.GetBool("isBurrowing"))
        {
            if(Input.GetKeyDown(KeyCode.Space) && playerJumping.clearAbove())
            {
                bc2D.size = new Vector2(0.6898985f, 0.96041f);
                bc2D.offset = new Vector2(-0.0001852f, -0.0108079f);
                anim.SetBool("isBurrowing", false);
                head.SetActive(true);
                body.SetActive(true);
                speed = 4f;
            }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            takeDamage();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            timeSlowed = !timeSlowed;
            if(timeSlowed)
            {
                Time.timeScale = 0.1f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void newCheckpoint(string checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    public void takeDamage()
    {
        if(!isInvincible && !anim.GetBool("isBurrowing"))
        {
            lives -= 1;
            if(lives <= 0)
            {
                cam.SetActive(false);
                hasDied();
            }
            if(flowerCount == 5)
            {   
                lives += 1;
                flowerCount = 0;
            }

            StartCoroutine(iFrames());
        }
    }

    public void flowerCollector()
    {
        flowerCount += 1;
        
        if(flowerCount >= 5)
        {
            if(lives < 5)
            {
                lives += 1;
                flowerCount = 0;
            }
            else
            {
                flowerCount = 5;
            }
        }
    }

    public void hasDied()
    {
        inputEnabled = false;
        Destroy(this.gameObject);
        sceneController.repeatLevel(lastCheckpoint);
    }

    public void gainBurrow()
    {
        canBurrow = true;
    }

    public void gainLava()
    {
        lavaImmunity = true;
    }

    public void gainJump()
    {
        extraJump = true;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer();
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        
        if(data != null)
        {
            lives = data.lives;
            lastCheckpoint = data.lastCheckpoint;
            flowerCount = data.flowerCount;
            canBurrow = data.canBurrow;
            lavaImmunity = data.lavaImmunity;
            extraJump = data.extraJump;
        }
    }

    IEnumerator iFrames()
    {
        isInvincible = true;

        for(float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            if(!anim.GetBool("isBurrowing"))
            {
                if(head.activeSelf && body.activeSelf)
                {
                    head.SetActive(false);
                    body.SetActive(false);
                }
                else
                {
                    head.SetActive(true);
                    body.SetActive(true);
                }
            }
            else
            {
                if(burrow.activeSelf)
                {
                    burrow.SetActive(false);
                }
                else
                {
                    burrow.SetActive(true);
                }
            }

            yield return new WaitForSeconds(invincibilityDeltaTime);
            
        }

        if(!anim.GetBool("isBurrowing"))
        {
            head.SetActive(true);
            body.SetActive(true);
        }
            burrow.SetActive(true);

        isInvincible = false;
    }
}
