using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource footsteps component
    public Rigidbody2D rb2D; // Reference to the critter

    void Start()
    {
        // Initialize the AudioSource and Rigidbody2D components if not set
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();

        audioSource.loop = true; // Set the audio to loop
    }

    void Update()
    {
        // Check if the player is moving
        if (Input.GetAxis("Horizontal") != 0) // Adjust the threshold as needed
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Start playing the loop if it is not already playing
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause(); // Pause the loop if the player stops moving
            }
        }
    }
}