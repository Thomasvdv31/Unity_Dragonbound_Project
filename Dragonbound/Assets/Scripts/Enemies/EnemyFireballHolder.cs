using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    public Transform enemy;

    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
