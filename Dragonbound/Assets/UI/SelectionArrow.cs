using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    public RectTransform[] options;
    public AudioClip moveSound;
    public AudioClip selectSound;
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // change position of arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            ChangePosition(1);
        }

        //Interact
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
            


    }

    private void ChangePosition(int change)
    {
        currentPosition += change;

        if (change != 0)
        {
            SoundManager.instance.PlaySound(moveSound);
        }

        if (currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        else if (currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0); // assign y position of the current option | up and down
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(selectSound);   

        options[currentPosition].GetComponent<Button>().onClick.Invoke();

    }
}
