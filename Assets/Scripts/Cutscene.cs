using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    private int i;
    public void Play() {
        // Iterate through all the children of the cutscene object, fading them in one after another

        gameObject.SetActive(true);
        foreach (Transform child in transform) {
            // Fade in the child
            StartCoroutine(
                AnimateSlide(child.GetComponent<Image>())
            );
        }
        gameObject.SetActive(false);
    }

    private IEnumerator AnimateSlide(Image image) {
        i = 0;
        for (i=0; i<100; i++) {
            image.color = new Color(1, 1, 1, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
