using System;
using UnityEngine;
using UnityEngine.UI;

public class ObstcleControler : MonoBehaviour
{
    [Range(1, 99999)] private int HP = 500;
    [SerializeField] private GameObject weaponConteiner;
    [SerializeField, Range(0, 1)] private float weaponChance = 0.5f;
    private Weapon weapon;

    private Text textForHP;
    static public int obstacleCount = 0;
    void Start()
    {
        obstacleCount++;
        HP = UnityEngine.Random.Range(obstacleCount, obstacleCount * 2 + 5);
        textForHP = GetComponentInChildren<Text>();
        textForHP.text = HP.ToString();
        if (UnityEngine.Random.value <= weaponChance && FirePoint.weaponsPrefabs.Length > 0)
        {
            weapon = Instantiate(FirePoint.weaponsPrefabs[UnityEngine.Random.Range(0, FirePoint.weaponsPrefabs.Length)], weaponConteiner.transform).GetComponent<Weapon>();
        }

        PlayerMovement.playerDie += resetStaticValues;

    }

    private void resetStaticValues()
    {
        obstacleCount = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "bullet")
        {
            HP -= FirePoint.bulletDamage;
            Destroy(collision.gameObject);
            if (HP <= 0)
            {
                if (weapon != null)
                {
                    FirePoint.bulletDamage = weapon.bulletDamage;
                    FirePoint.bulletInterval = weapon.bulletInterval;

                }

                Destroy(gameObject);
            }
            textForHP.text = HP.ToString();
        }
    }

    private void OnDestroy()
    {
        PlayerMovement.playerDie -= resetStaticValues;
    }

}
