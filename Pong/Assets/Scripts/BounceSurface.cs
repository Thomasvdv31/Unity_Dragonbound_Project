using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSurface : MonoBehaviour
{
    public float bounceStrength;
    public AudioSource audioPlayer;

    // Gets called when a collision happpens
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector2 normal = collision.GetContact(0).normal;
            ball.AddForce(-normal * this.bounceStrength); // add force to thhe opposite direction

            if(audioPlayer != null)
            {
                audioPlayer.Play();
            }
        }
    }

    
}
