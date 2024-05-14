using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles logic related to inputs and player movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Player Info")]
    [Tooltip("How fast the player moves")] public float MoveSpeed;
    [Tooltip("How high the player jumps")] public float JumpPower;
    
    [Header("Footstep Audio Assignments")]
    public AudioClip[] footstepNoises;
    public float footstepVolume;

    private Vector2 _initialScale;  // Used to flip the player left-right according to scale
    private Rigidbody2D _rb2D;  // Used to adjust the player's velocity
    private BoxCollider2D _boxCollider;  // Used for specific raycast information
    private AudioSource _audioSource;  // For playing sounds for footsteps
    private float _delaySinceLastFootstep;  // Recorded delay since last footstep SFX played

    private const float TIME_BETWEEN_FOOTSTEP_NOISES = 0.16f;  // Const for delay between footstep sounds


    private void Awake()
    {
        _initialScale = transform.localScale;
        _rb2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleXMovement();
        HandleYMovement();
    }

    /// <summary>
    /// Raycasts a box underneath the player. Returns True if touching an object with the "Ground"
    /// layer, else False.
    /// </summary>
    private bool IsGrounded() => Physics2D.BoxCast(transform.position, _boxCollider.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

    /// <summary>
    /// Handles the player's movement in the left-right direction.
    /// Uses velocity-based movement, so you won't be able to add forces.
    /// (We may decide to change this later!)
    /// </summary>
    private void HandleXMovement()
    {
        float horAxis = Input.GetAxis("Horizontal");
        if (horAxis != 0)
        {
            _rb2D.velocity = new Vector2(horAxis * MoveSpeed, _rb2D.velocity.y);
            if (horAxis < 0)
            {
                transform.localScale = new Vector2(-_initialScale.x, _initialScale.y);
            }
            else
            {
                transform.localScale = new Vector2(_initialScale.x, _initialScale.y);
            }
            // If the player is not in the air, play some footstep noises
            if (IsGrounded() && Time.time - _delaySinceLastFootstep > TIME_BETWEEN_FOOTSTEP_NOISES)
            {
                _audioSource.PlayOneShot(footstepNoises[Random.Range(0, footstepNoises.Length)], footstepVolume);
                _delaySinceLastFootstep = Time.time;
            }
        }
    }

    /// <summary>
    /// Handles the player's jumping mechanic.
    /// Checks for the "Jump" button, which can be configured in Project Settings > Input Manager.
    /// </summary>
    private void HandleYMovement()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, JumpPower);
        }
    }

}
