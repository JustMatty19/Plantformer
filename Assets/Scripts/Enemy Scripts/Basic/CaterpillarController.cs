using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarController : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    public GameObject Player;

    BoxCollider2D bc2D;
    Rigidbody2D rb2D;
    Animator anim;

    PlayerController playerController;

    float speed = 1.4f;

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
        anim.SetBool("isWalking", true);
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            enemyHitsPlayer();
            move();
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
            playerController.takeDamage();
        }
    }

    void move()
    {
        if(direction && anim.GetBool("isWalking"))
        {
            rb2D.MovePosition(rb2D.position + new Vector2(speed, 0) * Time.fixedDeltaTime);
            transform.localScale = left;
        }
        else if(anim.GetBool("isWalking"))
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

        yield return new WaitForSeconds(0.3f);

        anim.SetBool("isWalking", true);
    }
}
