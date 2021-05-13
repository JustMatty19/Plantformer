using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim.SetBool("isLeaving", false);
        StartCoroutine(wait());
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(go());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(30);

        anim.SetBool("isLeaving", true);

        yield return new WaitForSeconds(1f);

        anim.SetBool("isLeaving", false);

        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator go()
    {
        anim.SetBool("isLeaving", true);

        yield return new WaitForSeconds(1f);

        anim.SetBool("isLeaving", false);

        SceneManager.LoadScene("MainMenu");
    }
}
