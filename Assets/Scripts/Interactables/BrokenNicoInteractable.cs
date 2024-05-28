using UnityEngine;

public class BrokenNicoInteractable : Interactable
{
    private PlayerController _playerController;
    public Sprite freedNicoSprite;
    public AudioClip powerUpSound;
    public float powerUpVolume;
    private AudioSource audioSource;
    private void Start() {
        // Get a reference to the player controller
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            return;
        }
        _playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
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

        // Play turning on sound
        audioSource.PlayOneShot(powerUpSound, powerUpVolume);

        // Shift Nico into its next state
        Pickupable pickupable = gameObject.AddComponent<Pickupable>();
        pickupable.PopupObject = PopupObject; // Needed to set up the script properly
        // FIXME: Have to leave and reenter after giving battery in order to pick up
        // Swap out the sprite
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = freedNicoSprite;
        Destroy(this); // removes this script
    }

    public override bool ShouldShowPopup()
    {
        return _playerController.holding != null && _playerController.holding.name == "Battery";
    }
}