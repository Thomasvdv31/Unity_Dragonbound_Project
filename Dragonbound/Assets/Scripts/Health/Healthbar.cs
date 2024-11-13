using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health playerHealth;
    public Image totalHealthBar;
    public Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth._currentHealth / 10;
    }

    private void Update()
    {
        // 3 bars -> set fill to 0.3
        currentHealthBar.fillAmount = playerHealth._currentHealth / 10;
    }
}
