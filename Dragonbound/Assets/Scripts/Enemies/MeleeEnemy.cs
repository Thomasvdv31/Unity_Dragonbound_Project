using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack")]
    public float attackCooldown;
    public float range;
    public int damage;

    [Header("Collider")]
    public float colliderDistance;
    public BoxCollider2D boxCollider;

    [Header("Player Layer")]
    public LayerMask playerLayer;
    private float _cooldownTimer = Mathf.Infinity;

    [Header("Attack sound")]
    public AudioClip meleeSound;

    // refs
    private Animator _animator;
    private Health playerHealth;
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
            if (_cooldownTimer >= attackCooldown && playerHealth._currentHealth > 0)
            {
                _cooldownTimer = 0;
                _animator.SetTrigger("attack");
                SoundManager.instance.PlaySound(meleeSound);
            }
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight(); // enables patrol when the player is not in sight
        }

        
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            ,0,Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    // Draw a gizmo to see the attack box
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
