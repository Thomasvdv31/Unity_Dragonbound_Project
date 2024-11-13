using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;

    private float _cooldownTimer = Mathf.Infinity;

    public float attackCooldown;

    public Transform firePoint;
    public GameObject[] fireballs;
    public AudioClip fireballSound;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _cooldownTimer > attackCooldown && _playerMovement.CanAttack())
        {
            Attack();
        }

        _cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        _animator.SetTrigger("attack");
        _cooldownTimer = 0;

        // pooling fireball
        // Multiple fireballs already created
        // fireball deactivated on hit and waits to be reused.
        // good for creating lots of objects

        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
