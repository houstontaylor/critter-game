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

    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private Animator _playerAnimator;

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

    public override bool ShouldShowPopup()
    {
        return !OnlyWorksOnce || !_chewed;
    }


    /// <summary>
    /// toggle the laser this is connected to.
    /// </summary>
    public override void Interact()
    {
        if (_playerController.holding != null) return;  // Ignore if player is holding something
        if (OnlyWorksOnce && _chewed) return;  // Ignore if should only work once and was activated
        if (OnlyWorksOnce) PopupObject.SetActive(false);
       
        _chewed = true;
        PopupObject.SetActive(false);

        StartCoroutine(TemporarilyDisablePlayerMovementCoroutine());
        _playerAnimator.Play("Critter_Chew");  // Play the chewing animation on the player
            
        // Turns the wires gray; this can be deleted later
        GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);

        // Make all of the triggerables trigger
        if (TriggerablesToTrigger.Count > 0)
        {
            foreach (Triggerable triggerable in TriggerablesToTrigger)
            {
                triggerable.Interact();
            }
        }
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
