using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public static TimeText instance { get; private set; }
    private Text txt;
    private float timer; // This will track the time in the appropriate scenes
    private bool isRunning = true; // Flag to check if the timer is running
    
    private void Awake()
    {
        // Ensure that there's only one instance of TimeText
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate
            return;
        }

        instance = this; // Assign the static instance
        txt = GetComponentInChildren<Text>();
        timer = 0f; // Initialize timer to 0
    }

    private void Update()
    {
        if (IsSceneOneOrTwo() && isRunning) // Only update timer in scene 1 or 2
        {
            UpdateTimer(); // Update the timer variable
            UpdateTimerText(); // Update the displayed timer text
        }
    }

    private bool IsSceneOneOrTwo()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return activeSceneIndex == 1 || activeSceneIndex == 2;
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime; // Increment the timer by the time since the last frame
    }

    private void UpdateTimerText()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);

        // Format the timer text
        txt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Public method to start the timer
    public void StartTimer()
    {
        isRunning = true;
    }

    // Public method to stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }

    public float CurrentTime => timer;
}
