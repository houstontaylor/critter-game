using UnityEngine;

public abstract class ItemTaker : Interactable
{
    public PlayerController _playerController;
    public abstract string ItemName { get; }
    public void Start() {
        // Get a reference to the player controller
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            return;
        }
        _playerController = player.GetComponent<PlayerController>();
    }

    // returns whether the item was taken
    public bool TakeItem(bool consume = true)
    {
        // Get what the player is holding
        GameObject holding = _playerController.holding;
        if (holding == null) {
            return false;
        }

        // Check if the player is holding the item
        if (holding.name != ItemName) {
            return false;
        }

        // Consumes the item
        if (consume) {
            _playerController.ClearPickup();
        }

        return true;
    }

    public override bool ShouldShowPopup()
    {
        return _playerController.holding != null && _playerController.holding.name == ItemName;
    }
}
