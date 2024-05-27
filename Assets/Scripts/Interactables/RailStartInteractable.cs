using UnityEngine;


public class RailStartInteractable : Interactable
{
    public Rail rail;
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

        // Check if the player is holding Nico
        if (holding.name != "Nico") {
            return;
        }

        // Move Nico onto the rail
        rail.Mount(holding);
    }

    public override bool ShouldShowPopup()
    {
        return _playerController.holding != null && _playerController.holding.name == "Nico";
    }
}
