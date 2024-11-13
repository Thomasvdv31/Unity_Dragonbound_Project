using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [Header("sfx")]
    public AudioClip checkpointSound;

    private Transform currentCheckoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>(); // select first object
    }

    public void PlayerRespawn()
    {
        if(currentCheckoint == null)
        {
            uiManager.GameOver();
            return;
        }
        transform.position = currentCheckoint.position; // move to checkpoint position

        playerHealth.Respawn(); // reset animation and restore hp

        //move camera
        Camera.main.GetComponent<CameraControl>().MoveToAnotherRoom(currentCheckoint.parent);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "checkpoint")
        {
            currentCheckoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //deactivate checkpoint
            collision.GetComponent<Animator>().SetTrigger("appear");

        }
    }
}
