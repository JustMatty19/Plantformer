using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYLock : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 newCamPosition = new Vector3(target.position.x, 4.5f, -10f);
        gameObject.transform.position = newCamPosition;
    }
}
