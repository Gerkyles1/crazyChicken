using System;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    private GameObject currentBullet;

    private static float interval = 0.1f;
    private float timer = 0f;
    private static int bullet = 1;

    public static GameObject[] weaponsPrefabs;
    public static int bulletDamage
    {
        get { return bullet; }
        set { bullet = Math.Clamp(value, 1, 100); }
    }
    public static float bulletInterval
    {
        get { return interval; }
        set { interval = Math.Clamp(value, 0.01f, 3); }
    }

    private void Start()
    {

        weaponsPrefabs = Resources.LoadAll<GameObject>("Prefabs/Weapon");
        PlayerMovement.playerDie += resetStaticValue;


    }



    void Update()
    {
        if (!PlayerMovement.IsPlayerDie())
        {

            timer += Time.deltaTime;

            if (timer >= interval)
            {
                currentBullet = Instantiate(bulletPref, transform);
                currentBullet.transform.localPosition = Vector3.zero;
                currentBullet.transform.parent = null;

                timer = 0f;
            }
        }
    }

    private void resetStaticValue()
    {
        interval = 0.1f;
        bullet = 1;
    }
    private void OnDestroy()
    {
        PlayerMovement.playerDie -= resetStaticValue;
    }



}
