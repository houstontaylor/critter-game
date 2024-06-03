using UnityEngine;

public class BrokenNicoInteractable : ItemTaker
{
    public AudioClip powerUpSound;
    public float powerUpVolume;
    private AudioSource audioSource;

    public override string ItemName { get { return "Battery"; } }
    private new void Start() {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }
    public override void Interact()
    {
        if (!TakeItem()) {
            return;
        }

        // Play turning on sound
        audioSource.PlayOneShot(powerUpSound, powerUpVolume);

        // stop animation and swap out the sprite
        GetComponent<Animator>().SetBool("Freed", true);

        // Shift Nico into its next state
        // FIXME: Need to walk out and back in to pick up after giving battery
        Destroy(this); // removes this script
        Pickupable pickupable = gameObject.AddComponent<Pickupable>();
        pickupable.PopupObject = PopupObject; // Needed to set up the script properly
        pickupable.ShowPopup();  // You'll be touching Nico when you fix him, so...
    }

}