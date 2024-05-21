using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : Interactable
{
    public override void Interact()
    {
        // Get a reference to the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Make the player pick up the object
        player.GetComponent<PlayerController>().PickUp(gameObject);
    }
}
