using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMiniboss : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    public GameObject Player;

    public GameObject Fire_Invulnerability_Powerup;

    BoxCollider2D bc2D;
    Rigidbody2D rb2D;
    Animator anim;

    PlayerController playerController;
    Animator playerAnim;

    int lives = 3;
    bool damageTaken = false;
    bool ready = false;
    bool done = false;
    float speed = 6.5f;
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
        anim.SetBool("isFlying", true);
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            enemyHitsPlayer();
            if(!damageTaken)
            {
                target();
                move();
            }
            else if(damageTaken)
            {
                angry();
            }
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
            speed = 18f;
            transform.localScale = left;
            StartCoroutine(goToSpot());
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
        if(!ready)
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

            if(distance > 2.5)
            {
                direction = false;
            }
            else if(distance < -2.5)
            {
                direction = true;
            }
        }
    }

    void angry()
    {
        if(ready)
        {
            if(!done)
            {
                if(direction)
                {
                    rb2D.MovePosition(rb2D.position + new Vector2(speed, 0.1f) * Time.fixedDeltaTime);
                    transform.localScale = left;
                }
                else
                {
                    rb2D.MovePosition(rb2D.position + new Vector2(speed * -1, 0.1f) * Time.fixedDeltaTime);
                    transform.localScale = right;
                }
            }
            else
            {
                damageTaken = false;
                speed = 6.5f;
                StartCoroutine(backToOriginal());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBumpers")
        {
            direction = !direction;
            if(ready && !done && transform.position.y >= 0.7)
            {
                done = true;
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.6f);
        Fire_Invulnerability_Powerup.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        PlayerController.inputEnabled = true;
    }

    IEnumerator goToSpot()
    {
        while(Vector3.Distance(transform.position, new Vector3(16.07f, -0.294f, 0)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(16.07f, -0.294f, 0), speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        direction = true;
        ready = true;
    }

    IEnumerator backToOriginal()
    {
        while(Vector3.Distance(transform.position, new Vector3(transform.position.x, 0.22f, 0)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0.22f, 0), speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
        }

        ready = false;
        done = false;
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
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
