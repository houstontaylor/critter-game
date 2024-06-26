using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles logic related to inputs and player movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Player Info")]
    [Tooltip("How fast the player moves")] public float MoveSpeed;
    [Tooltip("How high the player jumps")] public float JumpPower;
    public bool CanPlayerMove
    {
        get => _canPlayerMove;
        set
        {
            _canPlayerMove = value;
            if (!_canPlayerMove)
            {
                _rb2D.velocity = Vector2.zero;  // Freeze the player if set to false
                _animator.SetFloat("xVelocity", 0);
            }
        }
    }
    
    [Header("Footstep Audio Assignments")]
    public AudioClip[] footstepNoises;
    public float footstepVolume;
    public AudioClip jumpSound;
    public float jumpVolume;
    private Vector2 _initialScale;  // Used to flip the player left-right according to scale
    private Rigidbody2D _rb2D;  // Used to adjust the player's velocity
    private BoxCollider2D _boxCollider;  // Used for specific raycast information
    private AudioSource _audioSource;  // For playing sounds for footsteps
    private float _delaySinceLastFootstep;  // Recorded delay since last footstep SFX played
    private Animator _animator;

    private const float TIME_BETWEEN_FOOTSTEP_NOISES = 0.16f;  // Const for delay between footstep sounds
    private const float ALLOWED_JUMP_HOLD_TIME = 0.3f;

    private bool _canPlayerMove = true;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _rb2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayerHorizontally();
        MovePlayerVertically();
    }

    /// <summary>
    /// Raycasts a box underneath the player. Returns True if touching an object with the "Ground"
    /// layer, else False.
    /// </summary>
    public bool IsGrounded() => Physics2D.BoxCast(transform.position, _boxCollider.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));

    /// <summary>
    /// Handles the player's movement in the left-right direction.
    /// Uses velocity-based movement, so you won't be able to add forces.
    /// (We may decide to change this later!)
    /// 
    /// If `horAxis` is set to 0, then take player movement normally.
    /// If `horAxis` is set to non-0, then move the player by that left-right velocity.
    /// </summary>
    public void MovePlayerHorizontally(float horAxis = 0)
    {
        if (!_canPlayerMove && horAxis == 0) { return; }
        if (horAxis == 0) horAxis = Input.GetAxis("Horizontal");
        _rb2D.velocity = new Vector2(horAxis * MoveSpeed, _rb2D.velocity.y);

        // Check direction and apply scale accordingly, but only if horAxis is not zero
        if (horAxis != 0)
        {
            if (horAxis < 0)
            {
                transform.localScale = new Vector2(-_initialScale.x, _initialScale.y);
            }
            else if (horAxis > 0)
            {
                transform.localScale = new Vector2(_initialScale.x, _initialScale.y);
            }

            // Play footstep sounds if the player is grounded and moving
            if (IsGrounded() && Time.time - _delaySinceLastFootstep > TIME_BETWEEN_FOOTSTEP_NOISES)
            {
                _audioSource.PlayOneShot(footstepNoises[Random.Range(0, footstepNoises.Length)], footstepVolume);
                _delaySinceLastFootstep = Time.time;
            }
        }

        _animator.SetFloat("xVelocity", Mathf.Abs(_rb2D.velocity.x));
    }

    /// <summary>
    /// Handles the player's jumping mechanic.
    /// Checks for the "Jump" button, which can be configured in Project Settings > Input Manager.
    /// </summary>
    private void MovePlayerVertically()
    {
        if (!_canPlayerMove) { return; }
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            StartCoroutine(RenderJumpCoroutine());
            _animator.Play("Critter_Jump");
            _audioSource.PlayOneShot(jumpSound, jumpVolume);
        }
    }

    /// <summary>
    /// Adds upward force to the player while the jump button is held, until a certain
    /// time is reached. Longer presses = higher jumps.
    /// </summary>
    private IEnumerator RenderJumpCoroutine()
    {
        float startTime = Time.time;
        while (Input.GetButton("Jump") && Time.time - startTime < ALLOWED_JUMP_HOLD_TIME)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, JumpPower);
            yield return null;
        }
    }

}
