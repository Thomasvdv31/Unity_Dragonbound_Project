using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected Rigidbody2D _rigiBody;
    public float speed = 10.0f;

    //  Get called one time in the lifecycle of the gameobject
    private void Awake()
    {
        //Search game objects with the script and that have rigibody
        _rigiBody = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition()
    {
        _rigiBody.position = new Vector2(_rigiBody.position.x, 0.0f);
        _rigiBody.velocity = Vector2.zero;
    }
}
