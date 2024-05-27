using UnityEngine;

public class BrokenRailInteractible : Interactable
{
    public RailControlPoint rail;
    private PlayerController _playerController;
    private void Start() {
        // Get a reference to the player controller
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            return;
        }
        _playerController = player.GetComponent<PlayerController>();
    }

    // If the player is holding a rail segment, consume it and advance the rail
    public override void Interact() {
        // Get what the player is holding
        GameObject holding = _playerController.holding;
        if (holding == null) {
            return;
        }

        // Check if the player is holding a rail segment
        if (holding.name != "RailSegment") {
            return;
        }

        // Consumes the item
        _playerController.ClearPickup();

        // Let the rail know it's repaired
        rail.Continue();
    }

    public override bool ShouldShowPopup()
    {
        return _playerController.holding != null && _playerController.holding.name == "RailSegment";
    }
}
