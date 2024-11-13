using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public float attackCooldown;
    public Transform firePoint;
    public GameObject[] fireArrows;

    [Header("sfx")]
    public AudioClip arrowSound;

    private float _cooldownTimer;

    private void Attack()
    {
        _cooldownTimer = 0;

        SoundManager.instance.PlaySound(arrowSound);
        fireArrows[FindFireArrow()].transform.position = firePoint.position;
        fireArrows[FindFireArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    private int FindFireArrow()
    {
        for (int i = 0; i < fireArrows.Length; i++)
        {
            if (!fireArrows[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if(_cooldownTimer >= attackCooldown)
        {

            Attack();

        }
    }
}
