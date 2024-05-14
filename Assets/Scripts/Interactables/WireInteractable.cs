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

    /// <summary>
    /// If this wire interactable is within range of the player, 
    /// and the player presses "E", then toggle the laser this
    /// is connected to.
    /// </summary>
    private void Update()
    {
        if (IsInteractable && Input.GetKeyDown(KeyCode.E))
        {
            if (OnlyWorksOnce)
            {
                ShouldShowPopup = false;  //  Hide popup if this has been interacted with
                                          //  and it should only be interacted with ONCE
                PopupObject.SetActive(false);
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

}
