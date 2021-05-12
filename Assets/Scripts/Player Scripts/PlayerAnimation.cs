using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    Animator animator;
    Rigidbody2D rb2D;
    PlayerMovement playerMovement;
    
    [Header ("Head Animation")]
    public SpriteRenderer base_SpriteRend;
    public SpriteRenderer idle1_SpriteRend;
    public SpriteRenderer idle2_SpriteRend;
    public SpriteRenderer walk_SpriteRend;
    public SpriteRenderer fall_SpriteRend;
    public SpriteRenderer jump_SpriteRend;

    public Sprite[] base_Array;
    public Sprite[] idle1_Array;
    public Sprite[] idle2_Array;
    public Sprite[] walk_Array;
    public Sprite[] fall_Array;
    public Sprite[] jump_Array;

    Vector3 left = new Vector3(-1, 1, 1);
    Vector3 right = new Vector3(1, 1, 1);

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            showDamageTaken();

            lastMovement();

            walkOrIdle();

            jumpOrFall();
        }
    }

    void showDamageTaken()
    {
        switch(PlayerController.lives)
        {
            case 1:
                base_SpriteRend.sprite = base_Array[0];
                idle1_SpriteRend.sprite = idle1_Array[0];
                idle2_SpriteRend.sprite = idle2_Array[0];
                walk_SpriteRend.sprite = walk_Array[0];
                fall_SpriteRend.sprite = fall_Array[0];
                jump_SpriteRend.sprite = jump_Array[0];
                break;
            case 2:
                base_SpriteRend.sprite = base_Array[1];
                idle1_SpriteRend.sprite = idle1_Array[1];
                idle2_SpriteRend.sprite = idle2_Array[1];
                walk_SpriteRend.sprite = walk_Array[1];
                fall_SpriteRend.sprite = fall_Array[1];
                jump_SpriteRend.sprite = jump_Array[1];
                break;
            case 3:
                base_SpriteRend.sprite = base_Array[2];
                idle1_SpriteRend.sprite = idle1_Array[2];
                idle2_SpriteRend.sprite = idle2_Array[2];
                walk_SpriteRend.sprite = walk_Array[2];
                fall_SpriteRend.sprite = fall_Array[2];
                jump_SpriteRend.sprite = jump_Array[2];
                break;
            case 4:
                base_SpriteRend.sprite = base_Array[3];
                idle1_SpriteRend.sprite = idle1_Array[3];
                idle2_SpriteRend.sprite = idle2_Array[3];
                walk_SpriteRend.sprite = walk_Array[3];
                fall_SpriteRend.sprite = fall_Array[3];
                jump_SpriteRend.sprite = jump_Array[3];
                break;
            case 5:
                base_SpriteRend.sprite = base_Array[4];
                idle1_SpriteRend.sprite = idle1_Array[4];
                idle2_SpriteRend.sprite = idle2_Array[4];
                walk_SpriteRend.sprite = walk_Array[4];
                fall_SpriteRend.sprite = fall_Array[4];
                jump_SpriteRend.sprite = jump_Array[4];
                break;
        }
    }

    void lastMovement()
    {
        if(playerMovement.lastMovement)
        {
            transform.localScale = right;
        }
        else
        {
            transform.localScale = left;
        }
    }

    void walkOrIdle()
    {
        if(Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))
        {
            if(PlayerController.inputEnabled)
            {
                isWalking();
            }
        }
        else
        {
            if(PlayerController.inputEnabled)
            {
                isIdle();
            }
        }
    }

    void jumpOrFall()
    {
        if(rb2D.velocity.y > 0.01)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        else if(rb2D.velocity.y < -0.01)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
    }

    public void isWalking()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
    }

    public void isIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", true);
    }
}
