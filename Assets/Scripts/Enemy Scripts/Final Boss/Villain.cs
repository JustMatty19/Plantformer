using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    public PauseMenuController pauseMenuController;

    public GameObject Player;

    public GameObject Royalty_Powerup;

    public GameObject caterpillar;
    public GameObject bee;
    public GameObject grasshopper;

    public GameObject top;
    public GameObject bottom;

    PlayerController playerController;

    BoxCollider2D bc2D;
    Animator anim;

    GameObject[] enemies = new GameObject[100];

    int lives = 4;

    int enemyIndex = 0;

    bool damageTaken = false;

    bool ready = false;

    Vector3 left = new Vector3(-1, 1, 1);
    Vector3 right = new Vector3(1, 1, 1);

    void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(!pauseMenuController.paused)
        {
            enemyHitsPlayer();
            if(ready)
            {
                StartCoroutine(controller());
                ready = false;
            }
        }
    }

    void enemyHitsPlayer()
    {
        float laserLength = 0.75f;
        Vector2 leftPosition = (Vector2)transform.position - new Vector2(bc2D.bounds.extents.x + 0.01f, bc2D.bounds.extents.y * -0.42f);
        Vector2 rightPosition = (Vector2)transform.position + new Vector2(bc2D.bounds.extents.x + 0.01f, -bc2D.bounds.extents.y * -0.42f);
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
            Debug.Log(lives);
            damageTaken = true;
            StartCoroutine(iFrames());
        }
        if(lives <= 0)
        {
            Debug.Log(enemyIndex);
            for(int i = 0; i < 100; i++)
            {
                if(enemies[i] != null)
                    enemies[i].gameObject.SetActive(false);
            }

            Destroy(bc2D);
            Destroy(anim);
            top.GetComponent<SpriteRenderer>().sprite = null;
            bottom.GetComponent<SpriteRenderer>().sprite = null;
            PlayerController.inputEnabled = false;
            Player.GetComponent<PlayerAnimation>().isIdle();
            StartCoroutine(wait());
        }
    }

    void moveSides(int spot)
    {
        if(spot == 1)
        {
            transform.localScale = right;
        }
        else if(spot == 0)
        {
            transform.localScale = left;
        }
    }

    void spawner(int spot)
    {
        if(lives == 4)
        {   
            GameObject enemy1 = gameObject;
            GameObject enemy2 = gameObject;

            if(spot == 0)
            {
                enemy1 = Instantiate(caterpillar, new Vector3(transform.position.x - 1f, -0.2519f, 1), transform.rotation);
                enemy2 = Instantiate(grasshopper, new Vector3(transform.position.x - 1f, -0.284f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<CaterpillarController>().direction = false;
                enemy2.gameObject.GetComponent<GrasshopperController>().direction = false;
            }
            if(spot == 1)
            {
                enemy1 = Instantiate(caterpillar, new Vector3(transform.position.x + 1f, -0.2519f, 1), transform.rotation);
                enemy2 = Instantiate(grasshopper, new Vector3(transform.position.x + 1f, -0.284f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<CaterpillarController>().direction = true;
                enemy2.gameObject.GetComponent<GrasshopperController>().direction = true;
            }

            enemies[enemyIndex] = enemy1;
            enemyIndex++;
            enemies[enemyIndex] = enemy2;
            enemyIndex++;
        }
        if(lives == 3)
        {
            GameObject enemy1 = gameObject;
            GameObject enemy2 = gameObject;
            GameObject enemy3 = gameObject;

            if(spot == 0)
            {
                enemy1 = Instantiate(caterpillar, new Vector3(transform.position.x - 1f, -0.2519f, 1), transform.rotation);
                enemy2 = Instantiate(grasshopper, new Vector3(transform.position.x - 1f, -0.284f, 1), transform.rotation);
                enemy3 = Instantiate(bee, new Vector3(transform.position.x - 1f, 0.221f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<CaterpillarController>().direction = false;
                enemy2.gameObject.GetComponent<GrasshopperController>().direction = false;
                enemy3.gameObject.GetComponent<BeeController>().direction = false;
            }
            if(spot == 1)
            {
                enemy1 = Instantiate(caterpillar, new Vector3(transform.position.x + 1f, -0.2519f, 1), transform.rotation);
                enemy2 = Instantiate(grasshopper, new Vector3(transform.position.x + 1f, -0.284f, 1), transform.rotation);
                enemy3 = Instantiate(bee, new Vector3(transform.position.x + 1f, 0.221f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<CaterpillarController>().direction = true;
                enemy2.gameObject.GetComponent<GrasshopperController>().direction = true;
                enemy3.gameObject.GetComponent<BeeController>().direction = true;
            }

            enemies[enemyIndex] = enemy1;
            enemyIndex++;
            enemies[enemyIndex] = enemy2;
            enemyIndex++;
            enemies[enemyIndex] = enemy3;
            enemyIndex++;
        }
        if(lives == 2)
        {
            GameObject enemy1 = gameObject;
            GameObject enemy2 = gameObject;
            GameObject enemy3 = gameObject;
            GameObject enemy4 = gameObject;

            if(spot == 0)
            {
                enemy1 = Instantiate(bee, new Vector3(transform.position.x - 1f, 0.221f, 1), transform.rotation);
                enemy2 = Instantiate(bee, new Vector3(transform.position.x - 2f, 0.221f, 1), transform.rotation);
                enemy3 = Instantiate(grasshopper, new Vector3(transform.position.x - 1f, -0.284f, 1), transform.rotation);
                enemy4 = Instantiate(grasshopper, new Vector3(transform.position.x - 2f, -0.284f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<BeeController>().direction = false;
                enemy2.gameObject.GetComponent<BeeController>().direction = false;
                enemy3.gameObject.GetComponent<GrasshopperController>().direction = false;
                enemy4.gameObject.GetComponent<GrasshopperController>().direction = false;
            }
            if(spot == 1)
            {
                enemy1 = Instantiate(bee, new Vector3(transform.position.x + 1f, 0.221f, 1), transform.rotation);
                enemy2 = Instantiate(bee, new Vector3(transform.position.x + 2f, 0.221f, 1), transform.rotation);
                enemy3 = Instantiate(grasshopper, new Vector3(transform.position.x + 1f, -0.284f, 1), transform.rotation);
                enemy4 = Instantiate(grasshopper, new Vector3(transform.position.x + 2f, -0.284f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<BeeController>().direction = true;
                enemy2.gameObject.GetComponent<BeeController>().direction = true;
                enemy3.gameObject.GetComponent<GrasshopperController>().direction = true;
                enemy4.gameObject.GetComponent<GrasshopperController>().direction = true;
            }

            enemies[enemyIndex] = enemy1;
            enemyIndex++;
            enemies[enemyIndex] = enemy2;
            enemyIndex++;
            enemies[enemyIndex] = enemy3;
            enemyIndex++;
            enemies[enemyIndex] = enemy4;
            enemyIndex++;
        }
        if(lives == 1)
        {
            GameObject enemy1 = gameObject;
            GameObject enemy2 = gameObject;
            GameObject enemy3 = gameObject;
            GameObject enemy4 = gameObject;
            GameObject enemy5 = gameObject;

            if(spot == 0)
            {
                enemy1 = Instantiate(caterpillar, new Vector3(transform.position.x - 1f, -0.2519f, 1), transform.rotation);
                enemy2 = Instantiate(caterpillar, new Vector3(transform.position.x - 2f, -0.2519f, 1), transform.rotation);
                enemy3 = Instantiate(bee, new Vector3(transform.position.x - 1f, 0.221f, 1), transform.rotation);
                enemy4 = Instantiate(bee, new Vector3(transform.position.x - 2f, 0.221f, 1), transform.rotation);
                enemy5 = Instantiate(grasshopper, new Vector3(transform.position.x - 1f, -0.284f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<CaterpillarController>().direction = false;
                enemy2.gameObject.GetComponent<CaterpillarController>().direction = false;
                enemy3.gameObject.GetComponent<BeeController>().direction = false;
                enemy4.gameObject.GetComponent<BeeController>().direction = false;
                enemy5.gameObject.GetComponent<GrasshopperController>().direction = false;
            }
            if(spot == 1)
            {
                enemy1 = Instantiate(caterpillar, new Vector3(transform.position.x + 1f, -0.2519f, 1), transform.rotation);
                enemy2 = Instantiate(caterpillar, new Vector3(transform.position.x + 2f, -0.2519f, 1), transform.rotation);
                enemy3 = Instantiate(bee, new Vector3(transform.position.x + 1f, 0.221f, 1), transform.rotation);
                enemy4 = Instantiate(bee, new Vector3(transform.position.x + 2f, 0.221f, 1), transform.rotation);
                enemy5 = Instantiate(grasshopper, new Vector3(transform.position.x + 1f, -0.284f, 1), transform.rotation);

                enemy1.gameObject.GetComponent<CaterpillarController>().direction = true;
                enemy2.gameObject.GetComponent<CaterpillarController>().direction = true;
                enemy3.gameObject.GetComponent<BeeController>().direction = true;
                enemy4.gameObject.GetComponent<BeeController>().direction = true;
                enemy5.gameObject.GetComponent<GrasshopperController>().direction = true;
            }

            enemies[enemyIndex] = enemy1;
            enemyIndex++;
            enemies[enemyIndex] = enemy2;
            enemyIndex++;
            enemies[enemyIndex] = enemy3;
            enemyIndex++;
            enemies[enemyIndex] = enemy4;
            enemyIndex++;
            enemies[enemyIndex] = enemy4;
            enemyIndex++;
        }
    }

    public void isReady()
    {
        ready = true;
    }

    IEnumerator controller()
    {
        while(anim != null)
        {
            anim.SetBool("goingRight", false);
            anim.SetBool("isPointing", true);

            spawner(0);

            yield return new WaitForSeconds(4f);

            anim.SetBool("isPointing", false);
            anim.SetBool("goingLeft", true);

            yield return new WaitForSeconds(1.7f);

            anim.SetBool("goingLeft", false);
            anim.SetBool("isPointing", true);

            moveSides(0);
            spawner(1);

            yield return new WaitForSeconds(4f);

            anim.SetBool("isPointing", false);

            moveSides(1);

            anim.SetBool("goingRight", true);

            yield return new WaitForSeconds(1.7f);
        }
    }

    IEnumerator iFrames()
    {
        for(float i = 0; i < 1.5f; i += 0.15f)
        {
            if(top.activeSelf && bottom.activeSelf)
            {
                top.SetActive(false);
                bottom.SetActive(false);
            }
            else
            {
                top.SetActive(true);
                bottom.SetActive(true);
            }

            yield return new WaitForSeconds(0.15f);
        }

        damageTaken = false;

        top.SetActive(true);
        bottom.SetActive(true);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.6f);
        Royalty_Powerup.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        PlayerController.inputEnabled = true;
    }
}
