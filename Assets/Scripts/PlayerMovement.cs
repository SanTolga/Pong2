using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private bool isAi;

    [SerializeField] private GameObject ball;

    private Rigidbody2D rb;
    private Vector2 playermove; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        if (isAi)
        {
            AiControl();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        playermove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void AiControl()
    {
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            playermove = new Vector2(0,1);
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            playermove = new Vector2(0, -1);
        }
        else
        {
            playermove = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playermove * moveSpeed; 
    }
}


