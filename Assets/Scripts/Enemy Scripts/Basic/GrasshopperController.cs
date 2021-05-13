using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasshopperController : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    public GameObject Player;

    BoxCollider2D bc2D;
    Rigidbody2D rb2D;
    Animator anim;

    PlayerController playerController;

    public bool direction = false;

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

        StartCoroutine(stopAndJump());
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBumpers")
        {
            direction = !direction;
        }
    }

    IEnumerator stopAndJump()
    {
        while(true)
        {
            anim.SetBool("isJumping", true);

            yield return new WaitForSeconds(0.1f);

            anim.SetBool("isJumping", false);

            yield return new WaitForSeconds(2);
        }
    }
}