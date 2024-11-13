using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    public float damage;
    public float activationDelay;
    public float activationTime;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    // when trap gets triggered
    private bool triggered;
    //when the trap is active and can hurt the player
    private bool active;

    private Health _playerHealth;

    [Header("sfx")]
    public AudioClip fireSound;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(_playerHealth != null && active)
        {
            _playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerHealth = collision.GetComponent<Health>();
            if(!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }

            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        //Notify player the trap is triggered
        triggered = true;
        _spriteRenderer.color = Color.red;

        //delayed active trap time
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireSound);
        _spriteRenderer.color = Color.white;
        active = true;
        _animator.SetBool("activated", true);

        // deactivate trap
        yield return new WaitForSeconds(activationTime);
        active = false;
        triggered = false;
        _animator.SetBool("activated", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _playerHealth = null;
        }
    }
}
