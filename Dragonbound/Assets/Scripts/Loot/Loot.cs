using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot", menuName = "Loot/Base Loot")]
public abstract class Loot : ScriptableObject
{
    public Sprite lootSprite;
    public string lootName;
    public int dropChance;


    public abstract void OnLoot(GameObject player);
}
