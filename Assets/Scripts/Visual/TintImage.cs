using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TintImage : MonoBehaviour
{
    public Color darkTint = Color.gray;
    public float firstDuration = 2f;
    public float secondDuration = 2f;
    public bool startDark = false;
    private Color normalColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;

        // Start the coroutine in Start method
        StartCoroutine(CycleColors());
    }

    IEnumerator CycleColors()
    {
        while (true)
        {
            // If startDark is true, start with darkTint, else start with normalColor
            Color startColor = startDark ? darkTint : normalColor;
            Color endColor = startDark ? normalColor : darkTint;

            // Set the color to startColor
            spriteRenderer.color = startColor;
            yield return new WaitForSeconds(firstDuration);

            // Set the color to endColor
            spriteRenderer.color = endColor;
            yield return new WaitForSeconds(secondDuration);
        }
    }
}