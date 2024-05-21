using UnityEngine;

public class Pickupable : Interactable {

    private void Update() {
        // Keep scale consistent even if parent flips
        Vector3 currentScale = transform.localScale;
        if (transform.lossyScale.x < 0) {
                currentScale.x *= -1;
        }
        transform.localScale = currentScale;
    }

    public override void Interact()
    {
        // Get a reference to the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Make the player pick up the object
        player.GetComponent<PlayerController>().PickUp(gameObject);
    }
}
