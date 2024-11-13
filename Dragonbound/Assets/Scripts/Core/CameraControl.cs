using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed;
    private float _currentPositionX;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        // changes a vector towards a desired goal over time
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);
    }

    public void MoveToAnotherRoom(Transform newRoom)
    {
        _currentPositionX = newRoom.position.x;
    }
}
