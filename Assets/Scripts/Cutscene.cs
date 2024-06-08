using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{

    [Header("Object Assignments")]
    public GameObject SlideBackground;
    [Header("Cutscene Properties")]
    public float SlideFadeInAnimationTime = 1;  // How long it will take to fade in the slide
    public float SlideShowTime = 2;  // How long the slide will be showing for

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    /// <summary>
    /// Iterate over all children of this object, showing them
    /// one after another with delays.
    /// </summary>
    public void Play() 
    {
        gameObject.SetActive(true);  // Show this object initially
        StartCoroutine(AnimateAllSlidesCoroutine());
    }

    private void OnEnable()
    {
        _playerMovement.CanPlayerMove = false;
    }

    private void OnDisable()
    {
        _playerMovement.CanPlayerMove = true;
    }

    /// <summary>
    /// Plays all of the slides in order, and waits for the last
    /// slide to fade out.
    /// 
    /// Hides the GameObject afterwards.
    /// </summary>
    private IEnumerator AnimateAllSlidesCoroutine()
    {
        // Initially hide all of the slides
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            // Show current child, make it invisible and start animation to fade in
            Image childImage = child.GetComponent<Image>();
            childImage.color = Color.clear;
            child.gameObject.SetActive(true);
            yield return AnimateSlideCoroutine(childImage);
            // If past the first child, show the background
            if (i == 0)
            {
                SlideBackground.SetActive(true);
            }
            // On the last child, wait for the last slide to fade out
            if (i == transform.childCount - 1)
            {
                SlideBackground.SetActive(false);
                yield return new WaitForSeconds(SlideFadeInAnimationTime);
            }
        }
        SlideBackground.SetActive(false);  // Hide the slide background just in case
        gameObject.SetActive(false);  // Re-hide this object afterwards
    }

    /// <summary>
    /// Handles the logic for animating a single slide.
    /// Returns after the slide has fully faded in; starts the coroutine
    /// to fade out after it has returned.
    /// </summary>
    private IEnumerator AnimateSlideCoroutine(Image img)
    {
        float currTime = 0;
        float timeToWait = SlideFadeInAnimationTime;
        Color initialColor = new(1, 1, 1, 0);
        while (currTime < timeToWait)
        {
            img.color = Color.Lerp(initialColor, Color.white, currTime / timeToWait);
            currTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(SlideShowTime);
        StartCoroutine(FadeOutSlideCoroutine(img));
    }

    /// <summary>
    /// Handles the logic for fading out a single slide.
    /// </summary>
    private IEnumerator FadeOutSlideCoroutine(Image img)
    {
        float currTime = 0;
        float timeToWait = SlideFadeInAnimationTime;
        Color targetColor = new(1, 1, 1, 0);
        while (currTime < timeToWait)
        {
            img.color = Color.Lerp(Color.white, targetColor, currTime / timeToWait);
            currTime += Time.deltaTime;
            yield return null;
        }
    }

}
