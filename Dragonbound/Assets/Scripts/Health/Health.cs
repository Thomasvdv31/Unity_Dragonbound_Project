using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float startHealth;
    public float _currentHealth { get; set; }

    private Animator _animator;

    private bool dead;

    [Header("iFrames")]
    public float iframeDuration;
    public int numberOfFlashes;
    private SpriteRenderer _spriteRend;


    [Header("Components")]
    public Behaviour[] _components;

    [Header("Death Sound")]
    public AudioClip deathSound;

    [Header("Hurt Sound")]
    public AudioClip hurtSound;

    private bool invulnerable;
    private LootBag lootBag;
    private void Awake()
    {
        _currentHealth = startHealth;
        _animator = GetComponent<Animator>();
        _spriteRend = GetComponent<SpriteRenderer>();
  

    }

    void Start()
    {
        lootBag = GetComponent<LootBag>(); // Get the LootBag component on start
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, startHealth);
        
        if(_currentHealth > 0)
        {
            
            _animator.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
            
        }
        else
        {
            
            if (!dead)
            {
                
                //Deactive all
                foreach (Behaviour comp in _components)
                {
                    if (comp is LootBag)
                    {
                        continue; // Skip disabling the specific script
                    }
                    comp.enabled = false;
                }
                
                _animator.SetBool("grounded", true);
                _animator.SetTrigger("die");
                GetComponent<LootBag>().InstantiateLoot(transform.position);
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
            
        }
    }

    public void AddHealth(float healthValue)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthValue, 0, startHealth);
    }

   

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8,9,true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            _spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
            _spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(8, 9, false);

        invulnerable = false;

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startHealth);
        _animator.ResetTrigger("die");
        _animator.Play("Idle");
        StartCoroutine(Invunerability());

        //active all
        foreach (Behaviour comp in _components)
        {
            comp.enabled = true;
        }
    }
    

    
}
