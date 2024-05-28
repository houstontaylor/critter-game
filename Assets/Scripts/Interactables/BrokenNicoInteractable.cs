using UnityEngine;

public class BrokenNicoInteractable : ItemTaker
{
    public Sprite freedNicoSprite;
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

        // Shift Nico into its next state
        Pickupable pickupable = gameObject.AddComponent<Pickupable>();
        pickupable.PopupObject = PopupObject; // Needed to set up the script properly
        // FIXME: Have to leave and reenter after giving battery in order to pick up
        // Swap out the sprite
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = freedNicoSprite;
        Destroy(this); // removes this script
    }
}