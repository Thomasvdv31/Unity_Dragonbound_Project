using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : Paddle
{
    public Rigidbody2D ball;

    private void FixedUpdate()
    {
        if(this.ball.velocity.x > 0.0f) //Ball moving to the right
        {
            if(this.ball.position.y > this.transform.position.y) //Check if ball is higher than paddle
            {
                _rigiBody.AddForce(Vector2.up * this.speed); //Go up
            }
            else if(this.ball.position.y < this.transform.position.y)
            {
                _rigiBody.AddForce(Vector2.down * this.speed); // go down
            }
            
        }
        else
        {
            if (this.transform.position.y > 0.0f) // if paddle up go back to center
            {
                _rigiBody.AddForce(Vector2.down * this.speed);
            }
            else if (this.transform.position.y < 0.0f) // if paddle down go back to center
            {
                _rigiBody.AddForce(Vector2.up * this.speed);
            }
        }
    }
}
