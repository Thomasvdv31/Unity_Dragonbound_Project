using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    public float speed;
    public float resetTime;
    private float lifeTime;

    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private bool hit;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        _boxCollider.enabled = true;
    }

    private void Update()
    {
        if(hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;

        if (lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
        _boxCollider.enabled = false;




        if (_animator != null)
        {
            _animator.SetTrigger("explode");
        }
        else
        {
            gameObject.SetActive(false); // for objects that have no explode animations
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
