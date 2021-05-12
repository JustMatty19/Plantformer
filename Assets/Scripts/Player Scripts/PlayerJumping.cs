using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    float jump;
    float fallMultiplier;
    float lowJumpMultiplier;

    [SerializeField]
    private bool extraJump = false;

    Animator anim;
    Rigidbody2D rb2D;
    BoxCollider2D bc2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(grounded())
        {
            if(PlayerController.extraJump)
            {
                extraJump = true;
            }
        }
        if(!pauseMenuController.paused)
        {
            jumping();
            clearAbove();
        }
    }

    void FixedUpdate()
    {
        if(!pauseMenuController.paused)
        {
            if(rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (PlayerController.fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if(rb2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (PlayerController.lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
    }

    void jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && PlayerController.inputEnabled)
        {
            if(grounded())
            {
                if(anim.GetBool("isBurrowing") && clearAbove())
                {
                    rb2D.velocity = new Vector2 (rb2D.velocity.x, PlayerController.jump);
                }
                
                if(!anim.GetBool("isBurrowing"))
                {
                    rb2D.velocity = new Vector2 (rb2D.velocity.x, PlayerController.jump);
                }
            }
            else if(extraJump)
            {
                rb2D.velocity = new Vector2 (rb2D.velocity.x, PlayerController.jump);
                extraJump = false;
            }
        }
    }

    public bool grounded()
    {
        Vector2 leftPosition;
        Vector2 middlePosition;
        Vector2 rightPosition;

        float laserLength = 0.03f;

        if(anim.GetBool("isBurrowing"))
        {
            leftPosition = (Vector2)transform.position - new Vector2((bc2D.bounds.extents.x - 0.05f), (bc2D.bounds.extents.y + 0.3960179f));
            middlePosition = (Vector2)transform.position - new Vector2(0, (bc2D.bounds.extents.y + 0.3960179f));
            rightPosition = (Vector2)transform.position + new Vector2((bc2D.bounds.extents.x - 0.05f), 0) - new Vector2(0, (bc2D.bounds.extents.y + 0.3960179f));
        }
        else
        {
            leftPosition = (Vector2)transform.position - new Vector2((bc2D.bounds.extents.x - 0.05f), (bc2D.bounds.extents.y + 0.015f));
            middlePosition = (Vector2)transform.position - new Vector2(0, (bc2D.bounds.extents.y + 0.015f));
            rightPosition = (Vector2)transform.position + new Vector2((bc2D.bounds.extents.x - 0.05f), 0) - new Vector2(0, (bc2D.bounds.extents.y + 0.015f));
        }

        int groundLayer = LayerMask.GetMask("Default");
        
        RaycastHit2D leftGroundCheck = Physics2D.Raycast(leftPosition, Vector2.down, laserLength, groundLayer);
        RaycastHit2D middleGroundCheck = Physics2D.Raycast(middlePosition, Vector2.down, laserLength, groundLayer);
        RaycastHit2D rightGroundCheck = Physics2D.Raycast(rightPosition, Vector2.down, laserLength, groundLayer);

        return leftGroundCheck.collider != null || middleGroundCheck.collider != null || rightGroundCheck.collider != null;
    }

    public bool clearAbove()
    {
        float laserLength = 0.03f;

        Vector2 leftPosition = (Vector2)transform.position - new Vector2(0.34494925f, 0.02f);
        Vector2 rightPosition = (Vector2)transform.position - new Vector2(-0.34494925f, 0.02f);

        int groundLayer = LayerMask.GetMask("Default");

        RaycastHit2D leftPositionCheck = Physics2D.Raycast(leftPosition, Vector2.up, laserLength, groundLayer);
        RaycastHit2D rightPositionCheck = Physics2D.Raycast(rightPosition, Vector2.up, laserLength, groundLayer);

        Debug.DrawRay(leftPosition, Vector2.up * laserLength, Color.red);
        Debug.DrawRay(rightPosition, Vector2.up * laserLength, Color.red);

        return leftPositionCheck.collider == null && rightPositionCheck.collider == null;
    }
}
