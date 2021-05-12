using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    Transform noPetals;
    Transform petal1;
    Transform petals2;
    Transform petals3;
    Transform petals4;
    Transform petals5;

    void Start()
    {
        noPetals = transform.GetChild(0);
        petal1 = transform.GetChild(1);
        petals2 = transform.GetChild(2);
        petals3 = transform.GetChild(3);
        petals4 = transform.GetChild(4);
        petals5 = transform.GetChild(5);

        noPetals.gameObject.SetActive(true);
        petal1.gameObject.SetActive(false);
        petals2.gameObject.SetActive(false);
        petals3.gameObject.SetActive(false);
        petals4.gameObject.SetActive(false);
        petals5.gameObject.SetActive(false);
    }
    void Update()
    {
        if(PlayerController.flowerCount == 0)
        {
            noPetals.gameObject.SetActive(true);
            petal1.gameObject.SetActive(false);
            petals2.gameObject.SetActive(false);
            petals3.gameObject.SetActive(false);
            petals4.gameObject.SetActive(false);
            petals5.gameObject.SetActive(false);
        }
        else if(PlayerController.flowerCount == 1)
        {
            noPetals.gameObject.SetActive(false);
            petal1.gameObject.SetActive(true);
            petals2.gameObject.SetActive(false);
            petals3.gameObject.SetActive(false);
            petals4.gameObject.SetActive(false);
            petals5.gameObject.SetActive(false);
        }
        else if(PlayerController.flowerCount == 2)
        {
            noPetals.gameObject.SetActive(false);
            petal1.gameObject.SetActive(false);
            petals2.gameObject.SetActive(true);
            petals3.gameObject.SetActive(false);
            petals4.gameObject.SetActive(false);
            petals5.gameObject.SetActive(false);
        }
        else if(PlayerController.flowerCount == 3)
        {
            noPetals.gameObject.SetActive(false);
            petal1.gameObject.SetActive(false);
            petals2.gameObject.SetActive(false);
            petals3.gameObject.SetActive(true);
            petals4.gameObject.SetActive(false);
            petals5.gameObject.SetActive(false);
        }
        else if(PlayerController.flowerCount == 4)
        {
            noPetals.gameObject.SetActive(false);
            petal1.gameObject.SetActive(false);
            petals2.gameObject.SetActive(false);
            petals3.gameObject.SetActive(false);
            petals4.gameObject.SetActive(true);
            petals5.gameObject.SetActive(false);
        }
        else if(PlayerController.flowerCount == 5)
        {
            noPetals.gameObject.SetActive(false);
            petal1.gameObject.SetActive(false);
            petals2.gameObject.SetActive(false);
            petals3.gameObject.SetActive(false);
            petals4.gameObject.SetActive(false);
            petals5.gameObject.SetActive(true);
        }
    }
}
