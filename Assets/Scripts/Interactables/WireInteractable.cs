using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireInteractable : Interactable
{

    [Header("Laser Assignment")]
    public Triggerable TriggerableToTrigger;
    public bool OnlyWorksOnce = true;  // Set to true if this should only trigger ONCE

    private bool _wasActivated = false;

    /// <summary>
    /// If this wire interactable is within range of the player, 
    /// and the player presses "E", then toggle the laser this
    /// is connected to.
    /// </summary>
    private void Update()
    {
        if (IsInteractable && Input.GetKeyDown(KeyCode.E))
        {
            if (TriggerableToTrigger != null && (OnlyWorksOnce && !_wasActivated))
            {
                _wasActivated = true;
                TriggerableToTrigger.Interact();
            }
        }
    }

}
