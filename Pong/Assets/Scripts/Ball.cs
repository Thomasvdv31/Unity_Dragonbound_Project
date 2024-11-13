using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public float speed = 200.0f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>(); // Search rigibody of gameobjects where ball script is linked to.
    }

    private void Start()
    {
        ResetBallPosition();
        AddStartingForce();
    }

    public void ResetBallPosition()
    {
        _rigidBody.position = Vector2.zero;
        _rigidBody.velocity = Vector2.zero;

        
    }


    public void AddStartingForce()
    {
        float xRandomDirectionValue =  Random.value < 0.5f ? -1.0f : 1.0f; // left or right
        float yRandomAngleValue = Random.value < 0.5f ? Random.Range(-1.0f, -0.25f) : Random.Range(0.25f, 1.0f); // up or down with a different angle

        Vector2 direction = new Vector2(xRandomDirectionValue, yRandomAngleValue);
        _rigidBody.AddForce(direction * speed);
    }

    public void AddForce(Vector2 force)
    {
        _rigidBody.AddForce(force);
    }

    
}
