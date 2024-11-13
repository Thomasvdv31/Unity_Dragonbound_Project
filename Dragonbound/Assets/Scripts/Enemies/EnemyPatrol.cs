using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol length")]
    public Transform leftEdge;
    public Transform rightEdge;

    [Header("Enemy")]
    public Transform enemy;

    [Header("Movement")]
    public float speed;

    [Header("Idle Time")]
    public float idleDuration;

    private float _idleTimer;

    private Vector3 _initScale;

    private bool _movingLeft;

    

    [Header("Enemy Animator")]
    public Animator animator;

    private void Awake()
    {
        _initScale = enemy.localScale;
    }

    private void OnDisable() //when disabled stop moving animation
    {
        animator.SetBool("moving", false);
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
            
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
            
        }
        
    }

    private void DirectionChange()
    {
        animator.SetBool("moving", false);

        _idleTimer += Time.deltaTime;

        if (_idleTimer > idleDuration)
        {
            _movingLeft = !_movingLeft;
        }
        
    }

    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        animator.SetBool("moving", true);
        // face direction
        enemy.localScale = new Vector3(Mathf.Abs(_initScale.x)  * direction, _initScale.y, _initScale.z);

        // move to that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.y);
    }
}
