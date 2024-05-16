using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireInteractable : Interactable
{

    [Header("Trigger Assignment")]
    public Triggerable TriggerableToTrigger;
    public bool OnlyWorksOnce = true;  // Set to true if this should only trigger ONCE

    private bool _wasActivated = false;

    private PlayerController _playerController;  // Player controller for status
    private PlayerMovement _playerMovement;  // Player movement to lock movement

    void Start()
    {
        // Find the player by tag and get the Animator component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerController = player.GetComponent<PlayerController>();
            _playerMovement = player.GetComponent<PlayerMovement>();
        }
    }


    /// <summary>
    /// If this wire interactable is within range of the player, 
    /// and the player presses "E", then toggle the laser this
    /// is connected to.
    /// </summary>
    private void Update()
    {
        if (IsInteractable && Input.GetKeyDown(KeyCode.E))
        {
            if (!OnlyWorksOnce || _wasActivated)
            {
                ShouldShowPopup = false;  //  Hide popup if this has been interacted with
                                          //  and it should only be interacted with ONCE
                PopupObject.SetActive(false);

                StartCoroutine(LockPlayerMovementUntilWireChewedCoroutine());

                // Turns the wires gray; this can be deleted later
                GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);  
            }
            if (TriggerableToTrigger != null && (!OnlyWorksOnce || !_wasActivated))
            {
                _wasActivated = true;
                TriggerableToTrigger.Interact();
            }
        }
    }

    private IEnumerator LockPlayerMovementUntilWireChewedCoroutine()
    {
        _playerController.PlayAnimation("Critter_Chew");  // Play the chew animation
        _playerMovement.CanMove = false;
        yield return new WaitForSeconds(1);
        _playerMovement.CanMove = true;

    }

}
