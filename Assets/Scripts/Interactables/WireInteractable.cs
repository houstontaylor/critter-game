using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireInteractable : Interactable
{
    [Header("Trigger Assignment")]
    public List<Triggerable> TriggerablesToTrigger;
    public bool OnlyWorksOnce = true;  // Set to true if this should only trigger ONCE

    public bool chewed = false;

    public Sprite wiresChewedSprite;

    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private Animator _playerAnimator;

    public override bool ShouldShowPopup() => !OnlyWorksOnce || !chewed;

    void Start()
    {
        // Find the player by tag and get the Animator component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerController = player.GetComponent<PlayerController>();
            _playerMovement = player.GetComponent<PlayerMovement>();
            _playerAnimator = player.GetComponent<Animator>();
        }
    }

    /// <summary>
    /// toggle the laser this is connected to.
    /// </summary>
    public override void Interact()
    {
        if (_playerMovement.IsGrounded() && _playerController.holding != null) return;  // Ignore if player is holding something
        if (OnlyWorksOnce && chewed) return;  // Ignore if should only work once and was activated
        if (OnlyWorksOnce) PopupObject.SetActive(false);

        chewed = true;
        PopupObject.SetActive(false);

        StartCoroutine(TemporarilyDisablePlayerMovementCoroutine());
        _playerAnimator.Play("Critter_Chew");  // Play the chewing animation on the player

        // Turns the wires gray; this can be deleted later
        ChangeSprite();

        // Make all of the triggerables trigger
        if (TriggerablesToTrigger.Count > 0)
        {
            foreach (Triggerable triggerable in TriggerablesToTrigger)
            {
                triggerable.Interact();
            }
        }
    }

    void ChangeSprite()
    {
        // Find the child object named "Wire Image"
        Transform wireImageTransform = transform.Find("Wire Image");
        if (wireImageTransform != null)
        {
            // Get the SpriteRenderer component from the child object
            SpriteRenderer wireImageRenderer = wireImageTransform.GetComponent<SpriteRenderer>();
            if (wireImageRenderer != null)
            {
                // Change the sprite to "wires_chewed"
                wireImageRenderer.sprite = wiresChewedSprite;
                wireImageRenderer.color = new Color(0.6f, 0.6f, 0.6f);
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on child object 'Wire Image'");
            }
        }
        else
        {
            Debug.LogError("Child object 'Wire Image' not found");
        }
    }


    public void Reset() {
        chewed = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator TemporarilyDisablePlayerMovementCoroutine()
    {
        _playerMovement.CanPlayerMove = false;
        yield return new WaitForSeconds(1);
        _playerMovement.CanPlayerMove = true;
    }

    /// <summary>
    /// Draw lines to everything this triggers.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (Triggerable obj in TriggerablesToTrigger)
        {
            Gizmos.DrawLine(transform.position, obj.transform.position);
        }
    }

}
