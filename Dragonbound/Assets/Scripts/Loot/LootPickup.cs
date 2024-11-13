using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour
{
    private Loot loot;


    // This method sets up the loot and player references
    public void Setup(Loot lootItem)
    {
        this.loot = lootItem;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(1, 1); // Set this based on your sprite's size
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            loot.OnLoot(collision.gameObject); // Call the custom behavior for the loot
            Destroy(gameObject); // Destroy the loot object after it's picked up
        }
    }
}
