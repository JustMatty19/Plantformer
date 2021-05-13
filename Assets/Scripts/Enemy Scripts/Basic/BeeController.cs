using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    public GameObject Player;

    BoxCollider2D bc2D;
    Rigidbody2D rb2D;
    Animator anim;

    PlayerController playerController;
    Animator playerAnim;

    float speed = 2f;

    public bool direction = false;

    bool targetSpotted = false;

    bool canMove = true;

    bool bypass = false;

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
        anim.SetBool("isFlying", true);
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            enemyHitsPlayer();
            target();
            move();
        }
    }

    void enemyHitsPlayer()
    {
        float laserLength = 0.14f;
        Vector2 leftPosition = (Vector2)transform.position - new Vector2(bc2D.bounds.extents.x, bc2D.bounds.extents.y * 0.2f);
        Vector2 rightPosition = (Vector2)transform.position + new Vector2(bc2D.bounds.extents.x, -bc2D.bounds.extents.y * 0.2f);
        int playerLayer = LayerMask.GetMask("Player");

        RaycastHit2D rightPlayerCheck = Physics2D.Raycast(rightPosition, Vector2.up, laserLength, playerLayer);
        RaycastHit2D leftPlayerCheck = Physics2D.Raycast(leftPosition, Vector2.up, laserLength, playerLayer);

        Debug.DrawRay(leftPosition, Vector2.up * laserLength, Color.red);
        Debug.DrawRay(rightPosition, Vector2.up * laserLength, Color.red);

        if(rightPlayerCheck.collider != null || leftPlayerCheck.collider != null)
        {
            playerController.takeDamage();
        }
    }

    void move()
    {
        if(targetSpotted && !canMove)
        {
            if(direction)
            {
                rb2D.MovePosition(rb2D.position + new Vector2(speed, 0) * Time.fixedDeltaTime);
                transform.localScale = left;
            }
            else
            {
                rb2D.MovePosition(rb2D.position + new Vector2(speed * -1, 0) * Time.fixedDeltaTime);
                transform.localScale = right;
            }
        }
        else if(canMove)
        {
            if(direction)
            {
                rb2D.MovePosition(rb2D.position + new Vector2(speed, 0) * Time.fixedDeltaTime);
                transform.localScale = left;
            }
            else
            {
                rb2D.MovePosition(rb2D.position + new Vector2(speed * -1, 0) * Time.fixedDeltaTime);
                transform.localScale = right;
            }
        }
    }

    void target()
    {
        if(Player != null && !playerAnim.GetBool("isBurrowing"))
        {
            float distance = transform.position.x - Player.transform.position.x;
            float level = transform.position.y - Player.transform.position.y;

            if(distance < 3 && distance > -3)
            {
                if(distance > 0.5 && level < 0.5 && level > -1)
                {
                    targetSpotted = true;
                    direction = false;
                }
                else if(distance < -0.5 && level < 0.5 && level > -1)
                {
                    targetSpotted = true;
                    direction = true;
                }
            }
            else
            {
                targetSpotted = false;
                canMove = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBumpers")
        {
            if(targetSpotted)
            {
                canMove = false;
            }
            else
            {
                if(!bypass)
                {
                    direction = !direction;
                }
                else
                {
                    bypass = false;
                }

                canMove = true;
            }
        }

        if(other.tag == "Ground")
        {
            direction = !direction;
            bypass = true;
        }
    }
}

