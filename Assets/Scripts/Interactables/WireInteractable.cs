using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireInteractable : Interactable
{

    [Header("Trigger Assignment")]
    public List<Triggerable> TriggerablesToTrigger;
    public bool OnlyWorksOnce = true;  // Set to true if this should only trigger ONCE

    private bool _chewed = false;
    private bool _triggeredAnything = false;

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

    public override bool ShouldShowPopup()
    {
        return !OnlyWorksOnce || !_chewed;
    }


    /// <summary>
    /// toggle the laser this is connected to.
    /// </summary>
    public override void Interact()
    {
        // Ignore if player is holding something
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<PlayerController>().holding != null)
        {
            return;
        }

        if (OnlyWorksOnce)
        {
            _chewed = true;
            PopupObject.SetActive(false);

            // plays chewing animation
            if (_playerAnimator != null)
            {
                _playerAnimator.Play("Critter_Chew");  // Play the chewing animation on the player
            }

            // Turns the wires gray; this can be deleted later
            GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
            // Make all of the triggerables trigger
            if (TriggerablesToTrigger.Count > 0 && (!OnlyWorksOnce || !_triggeredAnything))
            {
                foreach (Triggerable triggerable in TriggerablesToTrigger)
                {
                    _triggeredAnything = true;
                    triggerable.Interact();
                }
            }
        }
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
