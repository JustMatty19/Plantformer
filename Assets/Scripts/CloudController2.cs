using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController2 : MonoBehaviour
{
    public GameObject exitDoorTiles;

    Animator anim;

    bool ready = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!ready)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ready = true;
            }
        }
        else if(ready)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isReady", true);
                exitDoorTiles.SetActive(false);
            }
        }
    }
}