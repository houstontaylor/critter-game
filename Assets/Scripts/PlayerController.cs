using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Player Info")]
    [Tooltip("How fast the player moves")] public float MoveSpeed;
    [Tooltip("How high the player jumps")] public float JumpPower;
    
    private Vector2 _initialScale;  // Used to flip the player left-right according to scale
    private Rigidbody2D _rb2D;  // Used to adjust the player's velocity
    private BoxCollider2D _boxCollider;  // Used for specific raycast information

    private void Awake()
    {
        _initialScale = transform.localScale;
        _rb2D = GetComponent<Rigidbody2D>();
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
