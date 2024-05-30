using UnityEngine;

public abstract class ItemTaker : Interactable
{
    [HideInInspector] public PlayerController _playerController;
    public abstract string ItemName { get; }
    public void Start() {
        // Get a reference to the player controller
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            return;
        }
        _playerController = player.GetComponent<PlayerController>();
    }

    // Get the item the player is holding if it matches the item name
    public GameObject GetItem()
    {
        // Get what the player is holding
        GameObject holding = _playerController.holding;
        if (holding == null) {
            return null;
        }

        // Check if the player is holding the item
        if (holding.name != ItemName) {
            return null;
        }
        return holding;
    }

    // returns whether the item was taken
    public bool TakeItem(bool consume = true)
    {
        GameObject item = GetItem();
        if (item == null) {
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
