using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    public static Transform playerTransform;
    private float minX = -4.5f;
    private float maxX = 4.5f;
    private static bool die = false;
    public static event Action playerDie;

    private void Start()
    {
        die = false;
        playerTransform = transform;
    }

    void FixedUpdate()
    {
        if (!die)
        {
            if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X");
                MoveObject(mouseX);
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    float touchDeltaX = touch.deltaPosition.x;
                    MoveObject(touchDeltaX);
                }
            }
        }
        else
        {
            GetComponentInChildren<Animator>().SetTrigger("die");

        }
    }

    void MoveObject(float moveDelta)
    {
        Vector3 newPosition = transform.position + Vector3.right * moveDelta * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }

    public static bool IsPlayerDie()
    {
        return die;
    }
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "obstacle" || tag == "enemy")
        {
            die = true;
            GetComponentInChildren<Animator>().SetTrigger("die");
            playerDie?.Invoke();
        }
    }
}
