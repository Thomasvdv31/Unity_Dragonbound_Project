using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenuTitle : MonoBehaviour
{
    public Text totalTimeText; // Reference to the UI Text component

    private void Start()
    {
        DisplayTotalTime(); // Call the method to display the total time when the scene starts
    }

    private void DisplayTotalTime()
    {
        // Get the current time from TimeText singleton
        float totalTime = TimeText.instance.CurrentTime;

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(totalTime / 60F);
        int seconds = Mathf.FloorToInt(totalTime % 60F);

        // Format the timer text
        totalTimeText.text = string.Format("Total Time: {0:00}:{1:00}", minutes, seconds);
    }
}
