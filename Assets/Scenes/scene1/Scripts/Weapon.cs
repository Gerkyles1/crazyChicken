using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Range(1, 100)] public int bulletDamage = 1;
    [Range(0.01f, 3)] public float bulletInterval = 0.1f;

}

