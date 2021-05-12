﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighting : MonoBehaviour
{
    Rigidbody2D rb2D;
    BoxCollider2D bc2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        playerHitsEnemy();
    }

    void playerHitsEnemy()
    {
        float laserLength = 0.69f;
        Vector2 bottomPosition = (Vector2)transform.position - new Vector2(bc2D.bounds.extents.x, bc2D.bounds.extents.y * 1.3f);
        int enemyLayer = LayerMask.GetMask("Enemies");

        RaycastHit2D bottomEnemyCheck = Physics2D.Raycast(bottomPosition, Vector2.right, laserLength, enemyLayer);

        if(bottomEnemyCheck.collider != null)
        {
            if(rb2D.velocity.y < -0.01)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, PlayerController.jump);
                if(bottomEnemyCheck.transform.tag == "MiniBoss")
                {
                    bottomEnemyCheck.transform.gameObject.GetComponent<CaterpillarMiniboss>().takeDamage();
                }
                else
                {
                    Destroy(bottomEnemyCheck.transform.gameObject);
                }
            }
        }
    }
}
