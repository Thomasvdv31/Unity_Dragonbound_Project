using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : Paddle
{
    private Vector2 _direction;
    public Keyboard keyboard;

    // Called on startup
    private void Start()
    {
        keyboard = Keyboard.current;
    }

    // Gets called every frame - Good for tracking inputs etc
    private void Update()
    {
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            _direction = Vector2.up;
        }
        else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            _direction = Vector2.down;
        }
        else
        {
            _direction = Vector2.zero;
        }
    }

    // Called every fixed time interval - Good for physics based code
    private void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0) //Checking if the paddle is moving - 0 means not moving.
        {
            _rigiBody.AddForce(_direction * this.speed);
        }
    }
}
