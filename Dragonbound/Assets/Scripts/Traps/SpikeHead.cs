using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    public float speed;
    public float range;
    public float checkDelay;
    public LayerMask playerLayer;

    private float checkTimer;
    private Vector3 _destination;
    private Vector3[] _directions = new Vector3[4];
    private bool _attacking;

    [Header("sfx")]
    public AudioClip hitSound;

    // Method that gets called when an object gets activated
    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (_attacking) // move enemy to destination when attack mode is true.
        {
            transform.Translate(_destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < _directions.Length; i++)
        {
            Debug.DrawRay(transform.position, _directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _directions[i], range, playerLayer);

            if (hit.collider != null && !_attacking)
            {
                _attacking = true;
                _destination = _directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        _directions[0] = transform.right * range;
        _directions[1] = -transform.right * range;
        _directions[2] = transform.up * range;
        _directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        _destination = transform.position; // Set destination to current destination = stop
        _attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(hitSound);
        base.OnTriggerEnter2D(collision);

        //stop enemy when hit
        Stop();
    }
}
