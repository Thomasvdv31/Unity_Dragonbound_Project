using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jump Buff", menuName = "Loot/Jump Buff")]
public class JumpBuff : Loot
{
    public AudioClip pickUpSound;
    public float jumpIncreaseAmount = 10f;

    // This method will be called when the player picks up the loot.
    public override void OnLoot(GameObject player)
    {
        // Play sound and increase the player's jump speed
        if (pickUpSound != null)
        {
            SoundManager.instance.PlaySound(pickUpSound);
        }

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.jumpSpeed += jumpIncreaseAmount;
        }
    }
}
