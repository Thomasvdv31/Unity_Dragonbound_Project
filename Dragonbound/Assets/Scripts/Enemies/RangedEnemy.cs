using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack")]
    public float attackCooldown;
    public float range;
    public int damage;

    [Header("Ranged Attack")]
    public Transform firepoint;
    public GameObject[] fireballs;

    [Header("Collider")]
    public float colliderDistance;
    public BoxCollider2D boxCollider;

    [Header("Player Layer")]
    public LayerMask playerLayer;
    private float _cooldownTimer = Mathf.Infinity;

    [Header("Fireball Sound")]
    public AudioClip fireballSound;


    // refs
    private Animator _animator;
    private EnemyPatrol _enemyPatrol;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (_cooldownTimer >= attackCooldown)
            {
                _cooldownTimer = 0;
                _animator.SetTrigger("rangedAttack");
            }
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight(); // enables patrol when the player is not in sight
        }


    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        _cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
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

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            , 0, Vector2.left, 0, playerLayer);

        
        return hit.collider != null;
    }

    // Draw a gizmo to see the attack box
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
