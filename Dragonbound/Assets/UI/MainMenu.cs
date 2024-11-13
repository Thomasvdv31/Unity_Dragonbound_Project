using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGameEasyMode()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGameHardMode()
    {
        SceneManager.LoadScene(2);
    }


    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
