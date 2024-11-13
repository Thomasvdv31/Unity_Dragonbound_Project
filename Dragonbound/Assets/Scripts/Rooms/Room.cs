using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] enemies;
    private Vector3[] _enemyPositions;

    private void Awake()
    {
        //Save positions
        _enemyPositions = new Vector3[enemies.Length];

        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                _enemyPositions[i] = enemies[i].transform.position;
            }
            
        }
    }

    public void ActivateRoom(bool status)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(status);
                enemies[i].transform.position = _enemyPositions[i];
            }

        }
    }
}
