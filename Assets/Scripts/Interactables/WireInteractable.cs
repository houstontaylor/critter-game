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

    // player animation 
    private Animator _playerAnimator;

    void Start()
    {
        // Find the player by tag and get the Animator component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerAnimator = player.GetComponent<Animator>();
        }
    }


    /// <summary>
    /// toggle the laser this is connected to.
    /// </summary>
    public override void Interact()
    {
        if (OnlyWorksOnce)
            {
                ShouldShowPopup = false;  //  Hide popup if this has been interacted with
                                          //  and it should only be interacted with ONCE
                PopupObject.SetActive(false);

                // plays chewing animation
                if (_playerAnimator != null)
                {
                    _playerAnimator.Play("Critter_Chew");  // Play the chewing animation on the player
                }

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
