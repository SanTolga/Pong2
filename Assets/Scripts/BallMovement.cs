using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerScore; 
    [SerializeField] private Text aiScore;
    private int _hitCounter;
    private Rigidbody2D rb; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall",2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed+ (speedIncrease*_hitCounter) );
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * _hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        _hitCounter = 0; 
        Invoke("StartBall",2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        _hitCounter++;
        Vector2 ballpos = transform.position;
        Vector2 playerpos = myObject.position;
        float xDirection, yDirection;
        if (transform.position.x >0 )
        {
            xDirection = -1; 
        }
        else
        {
            xDirection = 1; 
        }

        yDirection = (ballpos.y - playerpos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * _hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "Ai")
        {
            PlayerBounce(other.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > 0) 
        {
            ResetBall();
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString(); 
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            aiScore.text = (int.Parse(aiScore.text) + 1).ToString(); 
        }
    }
}