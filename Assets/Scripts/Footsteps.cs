using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource footsteps component
    public Rigidbody2D rb2D; // Reference to the critter

    public AudioClip[] footstepNoises;
    public float footstepVolume;
    private float _delaySinceLastFootstep;

    private const float TIME_BETWEEN_FOOTSTEP_NOISES = 0.16f;

    void Start()
    {
        // Initialize the AudioSource and Rigidbody2D components if not set
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is moving
        if (Input.GetAxis("Horizontal") != 0 && Time.time - _delaySinceLastFootstep > TIME_BETWEEN_FOOTSTEP_NOISES) // Adjust the threshold as needed
        {
            audioSource.PlayOneShot(footstepNoises[Random.Range(0, footstepNoises.Length)], footstepVolume);
            _delaySinceLastFootstep = Time.time;
        }
    }
}