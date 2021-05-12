using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    Rigidbody2D rb2D;
    BoxCollider2D bc2D;
    Animator anim;

    float moveVelocity;

    public bool lastMovement = true;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            movement();
        }
    }

    void movement()
    {
        if(!anim.GetBool("isBurrowing"))
        {
            moveVelocity = 0;

            //Left Right Movement
            if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) && PlayerController.inputEnabled) 
            {
                moveVelocity = -PlayerController.speed;
                lastMovement = false;
            }
            if ((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) && PlayerController.inputEnabled) 
            {
                moveVelocity = PlayerController.speed;
                lastMovement = true;
            }

            rb2D.velocity = new Vector2 (moveVelocity, rb2D.velocity.y);
        }
        else
        {
            if(notGrounded() == -1)
            {
                moveVelocity = 0;

                if ((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) && PlayerController.inputEnabled) 
                {
                    moveVelocity = PlayerController.speed;
                    lastMovement = true;
                }

                rb2D.velocity = new Vector2 (moveVelocity, rb2D.velocity.y);
            }

            else if(notGrounded() == 1)
            {
                moveVelocity = 0;

                if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) && PlayerController.inputEnabled) 
                {
                    moveVelocity = -PlayerController.speed;
                    lastMovement = false;
                }

                rb2D.velocity = new Vector2 (moveVelocity, rb2D.velocity.y);
            }

            else if(notGrounded() == 2)
            {
                moveVelocity = 0;
            }

            else
            {
                moveVelocity = 0;

                //Left Right Movement
                if ((Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) && PlayerController.inputEnabled) 
                {
                    moveVelocity = -PlayerController.speed;
                    lastMovement = false;
                }
                if ((Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) && PlayerController.inputEnabled) 
                {
                    moveVelocity = PlayerController.speed;
                    lastMovement = true;
                }

                rb2D.velocity = new Vector2 (moveVelocity, rb2D.velocity.y);
                }
        }
    }

    public int notGrounded()
    {
        Vector2 leftPosition;
        Vector2 rightPosition;

        float laserLength = 0.03f;

        if(anim.GetBool("isBurrowing"))
        {
            leftPosition = (Vector2)transform.position - new Vector2(bc2D.bounds.extents.x , (bc2D.bounds.extents.y + 0.3960179f));
            rightPosition = (Vector2)transform.position + new Vector2(bc2D.bounds.extents.x, 0) - new Vector2(0, (bc2D.bounds.extents.y + 0.3960179f));
        }
        else
        {
            leftPosition = (Vector2)transform.position - new Vector2(bc2D.bounds.extents.x , (bc2D.bounds.extents.y + 0.015f));
            rightPosition = (Vector2)transform.position + new Vector2(bc2D.bounds.extents.x, 0) - new Vector2(0, (bc2D.bounds.extents.y + 0.015f));
        }
        
        int groundLayer = LayerMask.GetMask("Default");
        
        RaycastHit2D leftGroundCheck = Physics2D.Raycast(leftPosition, Vector2.down, laserLength, groundLayer);
        RaycastHit2D rightGroundCheck = Physics2D.Raycast(rightPosition, Vector2.down, laserLength, groundLayer);

        if(leftGroundCheck.collider == null && rightGroundCheck.collider == null)
        {
            return 2;
        }
        else if(leftGroundCheck.collider == null)
        {
            return -1;
        }
        else if(rightGroundCheck.collider == null)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
