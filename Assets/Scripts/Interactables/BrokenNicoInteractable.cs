using UnityEngine;

public class BrokenNicoInteractable : Interactable
{
    private PlayerController _playerController;
    private void Start() {
        // Get a reference to the player controller
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            return;
        }
        _playerController = player.GetComponent<PlayerController>();
    }
    public override void Interact()
    {
        // Get what the player is holding
        GameObject holding = _playerController.holding;
        if (holding == null) {
            return;
        }

        // Check if the player is holding a battery
        if (holding.name != "Battery") {
            return;
        }

        // Consumes the battery
        _playerController.ClearPickup();

        // Shift Nico into its next state
        Pickupable pickupable = gameObject.AddComponent<Pickupable>();
        pickupable.PopupObject = PopupObject; // Needed to set up the script properly
        // FIXME: Have to leave and reenter after giving battery in order to pick up
        Destroy(this); // removes this script
    }

    public override bool ShouldShowPopup()
    {
        return _playerController.holding != null && _playerController.holding.name == "Battery";
    }
}