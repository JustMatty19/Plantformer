using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject CameraController;

    Animator animator;

    void Awake()
    {
        animator = CameraController.GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "CameraSwitch")
            animator.Play("JumpingCamera");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "CameraSwitch")
            animator.Play("FlatCamera");
    }
}
