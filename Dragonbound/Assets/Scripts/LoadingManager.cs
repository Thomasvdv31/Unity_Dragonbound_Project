using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // press f to change level (PLACEHOLDR)
        {
            SceneManager.LoadScene(1);
        }
    }
}
