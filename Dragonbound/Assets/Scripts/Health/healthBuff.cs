using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Buff", menuName = "Loot/Health Buff")]
public class healthBuff : Loot
{
    public AudioClip pickUpSound;
    public float healthIncreaseAmount = 1f;

    // This method will be called when the player picks up the loot.
    public override void OnLoot(GameObject player)
    {
        // Play sound and increase the player's jump speed
        if (pickUpSound != null)
        {
            SoundManager.instance.PlaySound(pickUpSound);
        }

        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.startHealth += 1;
            playerHealth._currentHealth += (playerHealth.GetComponent<Health>().startHealth - playerHealth.GetComponent<Health>()._currentHealth);
        }
    }
}



