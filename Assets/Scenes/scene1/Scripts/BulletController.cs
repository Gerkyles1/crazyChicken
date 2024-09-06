using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * 80 * Time.deltaTime);
    }
}


