using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public AudioClip winMusic;
    private GameObject _winobject;
    private bool hasTriggered = false;


    private void Awake()
    {
        _winobject = GetComponent<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered) // Check if the trigger hasn't been activated
        {
            hasTriggered = true; // Set the flag to true to prevent further triggers

            // Pause 
            SoundManager.instance.PauseMusic();

            SoundManager.instance.PlaySound(winMusic);
            StartCoroutine(DelaySceneTransition());
        }
    }

    private IEnumerator DelaySceneTransition()
    {
        
        // Wait for 7 seconds

        yield return new WaitForSeconds(7f);
    

        // Resume the main music after the delay
        SoundManager.instance.ResumeMusic();

        // Load the next scene
        SceneManager.LoadScene(3);
    }
}
