using UnityEngine;

public class Nico : Interactable
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

        // Check if the player is holding a hard drive
        if (holding.name != "HardDrive") {
            return;
        }

        // Consumes the hard drive
        _playerController.ClearPickup();

        // TODO: Trigger cutscene and advance rail
        // need a reference to the stop we're moving past
    }

    public override bool ShouldShowPopup()
    {
        return _playerController.holding != null && _playerController.holding.name == "HardDrive";
    }
}