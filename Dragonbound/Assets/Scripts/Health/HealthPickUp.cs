using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float Restorehealth;

    [Header("sfx")]
    public AudioClip pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickUpSound);
            collision.GetComponent<Health>().AddHealth(Restorehealth);
            gameObject.SetActive(false);
        }
    }
}
