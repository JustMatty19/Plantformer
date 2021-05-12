using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(wait());
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(30);

        SceneManager.LoadScene("MainMenu");
    }
}
