using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasshopperMiniboss : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    public GameObject Player;

    public GameObject Jump_Powerup;

    BoxCollider2D bc2D;
    Rigidbody2D rb2D;
    Animator anim;

    PlayerController playerController;
    Animator playerAnim;

    int lives = 3;

    bool ready = false;

    bool first = true;

    bool damageTaken = false;

    bool direction = false;

    Vector3 left = new Vector3(-1, 1, 1);
    Vector3 right = new Vector3(1, 1, 1);

    void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
        playerAnim = Player.GetComponent<Animator>();
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            enemyHitsPlayer();
            move();
            target();
            if(ready && first)
            {
                StartCoroutine(stopAndJump());
                first = false;
            }
        }
    }

    void enemyHitsPlayer()
    {
        float laserLength = 0.24f;
        Vector2 leftPosition = (Vector2)transform.position - new Vector2(bc2D.bounds.extents.x + 0.01f, bc2D.bounds.extents.y * -0.2f);
        Vector2 rightPosition = (Vector2)transform.position + new Vector2(bc2D.bounds.extents.x + 0.01f, -bc2D.bounds.extents.y * -0.2f);
        int playerLayer = LayerMask.GetMask("Player");

        RaycastHit2D rightPlayerCheck = Physics2D.Raycast(rightPosition, Vector2.down, laserLength, playerLayer);
        RaycastHit2D leftPlayerCheck = Physics2D.Raycast(leftPosition, Vector2.down, laserLength, playerLayer);

        Debug.DrawRay(leftPosition, Vector2.down * laserLength, Color.red);
        Debug.DrawRay(rightPosition, Vector2.down * laserLength, Color.red);

        if(rightPlayerCheck.collider != null || leftPlayerCheck.collider != null)
        {
            playerController.takeDamage();
        }
    }

    public void takeDamage()
    {
        if(!damageTaken)
        {
            lives -= 1;
            damageTaken = true;
            StartCoroutine(iFrames());
        }
        if(lives == 0)
        {
            Destroy(bc2D);
            Destroy(anim);
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            PlayerController.inputEnabled = false;
            Player.GetComponent<PlayerAnimation>().isIdle();
            StartCoroutine(wait());
        }
    }

    void move()
    {
        if(direction)
        {
            transform.localScale = left;
        }
        else
        {
            transform.localScale = right;
        }
    }

    void target()
    {
        if(Player != null && !playerAnim.GetBool("isBurrowing"))
        {
            float distance = transform.position.x - Player.transform.position.x;

            if(distance > 1.5)
            {
                direction = false;
            }
            else if(distance < -1.5)
            {
                direction = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBumpers")
        {
            direction = !direction;
        }
    }

    public void readyUp()
    {
        ready = true;
    }

    IEnumerator stopAndJump()
    {
        while(true)
        {
            anim.SetBool("isJumpingTwice", true);

            yield return new WaitForSeconds(1.31f);

            anim.SetBool("isJumpingTwice", false);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.6f);
        Jump_Powerup.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        PlayerController.inputEnabled = true;
    }

    IEnumerator iFrames()
    {
        for(float i = 0; i < 1.5f; i += 0.15f)
        {
            if(gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }

            yield return new WaitForSeconds(0.15f);
        }

        damageTaken = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}