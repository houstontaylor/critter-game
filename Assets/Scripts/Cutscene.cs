using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public void Play() {
        // TODO: Iterate through all the children of the cutscene object, fading them in one after another
        Debug.Log("Playing cutscene");

        enabled = true;
        foreach (Transform child in transform) {
            // Fade in the child
            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1, 1, 1, 0);
            // TODO: Animate
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        enabled = false;
    }
}
