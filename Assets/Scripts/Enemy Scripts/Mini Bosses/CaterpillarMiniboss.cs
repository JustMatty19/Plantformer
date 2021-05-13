using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarMiniboss : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    public GameObject Player;

    public GameObject Burrow_Powerup;

    BoxCollider2D bc2D;
    Rigidbody2D rb2D;
    Animator anim;

    PlayerController playerController;

    float speed = 4.2f;

    bool direction = false;
    bool follow = false;
    bool damageTaken = false;
    bool stop = false;
    bool coroutineStarted = false;

    int lives = 3;

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
        anim.SetBool("isWalking", true);
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            if(!follow)
            {
                enemyHitsPlayer();
                move();
            }
            else if(follow)
            {
                following();
            }
            else if(stop)
            {
                enemyHitsPlayer();
            }
        }
    }

    void enemyHitsPlayer()
    {
        float laserLength = 0.14f;
        Vector2 leftPosition = (Vector2)transform.position - new Vector2(bc2D.bounds.extents.x, bc2D.bounds.extents.y * 2.2f);
        Vector2 rightPosition = (Vector2)transform.position + new Vector2(bc2D.bounds.extents.x, -bc2D.bounds.extents.y * 2.2f);
        int playerLayer = LayerMask.GetMask("Player");

        RaycastHit2D rightPlayerCheck = Physics2D.Raycast(rightPosition, Vector2.up, laserLength, playerLayer);
        RaycastHit2D leftPlayerCheck = Physics2D.Raycast(leftPosition, Vector2.up, laserLength, playerLayer);

        if(rightPlayerCheck.collider != null || leftPlayerCheck.collider != null)
        {
            if(!damageTaken)
            {
                playerController.takeDamage();
            }
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
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
            PlayerController.inputEnabled = false;
            Player.GetComponent<PlayerAnimation>().isIdle();
            StartCoroutine(wait());
        }
        else
        {
            anim.SetBool("isBurrowing", true);
            follow = true;
        }
    }

    void following()
    {
        if(!coroutineStarted)
        {
            StartCoroutine(followTimer());
        }

        if(Player != null)
        {
            float distance = transform.position.x - Player.transform.position.x;

            if(distance > 0.3)
            {
                direction = false;
            }
            else if(distance < -0.3)
            {
                direction = true;
            }
        }

        if(speed != 0)
        {
            move();
        }
    }

    void move()
    {
        if(direction && (anim.GetBool("isWalking") || anim.GetBool("isBurrowing")))
        {
            rb2D.MovePosition(rb2D.position + new Vector2(speed, 0) * Time.fixedDeltaTime);
            transform.localScale = left;
        }
        else if(anim.GetBool("isWalking") || anim.GetBool("isBurrowing"))
        {
            rb2D.MovePosition(rb2D.position + new Vector2(speed * -1, 0) * Time.fixedDeltaTime);
            transform.localScale = right;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBumpers")
        {
            StartCoroutine(stopAndTurn());
            direction = !direction;
        }
    }

    IEnumerator stopAndTurn()
    {
        anim.SetBool("isWalking", false);

        yield return new WaitForSeconds(0.05f);

        anim.SetBool("isWalking", true);
    }

    IEnumerator followTimer()
    {
        coroutineStarted = true;

        speed = 4f;
        Debug.Log("Following");

        yield return new WaitForSeconds(4);
        
        speed = 0;
        Debug.Log("Stopping");
        stop = true;
        follow = false;

        yield return new WaitForSeconds(0.2f);

        coroutineStarted = false;
        anim.SetBool("isBurrowing", false);
        anim.SetBool("isWalking", false);
        Debug.Log("Popping out");

        yield return new WaitForSeconds(0.2f);

        stop = false;
        damageTaken = false;
        anim.SetBool("isWalking", true);
        speed = 4.2f;
        Debug.Log("Walking again");
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.6f);
        Burrow_Powerup.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        PlayerController.inputEnabled = true;
    }

    IEnumerator iFrames()
    {
        for(float i = 0; i < 1.5f; i += 0.15f)
        {
            if(gameObject.GetComponentInChildren<SpriteRenderer>().enabled == true)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }

            yield return new WaitForSeconds(0.15f);
        }
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
