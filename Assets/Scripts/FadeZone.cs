using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class FadeZone : MonoBehaviour
{

    [Header("Fade Properties")]
    public float HardZoneDistanceFromCenter = 0.4f;  // Distance from center player must
                                                     // pass through until full fade to black

    private Image _globalFadeImage;  // Image that covers entire screen to fade
    private PlayerController _playerController;
    private float _objectWidth;

    private void Awake()
    {
        _objectWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _globalFadeImage = GameObject.FindGameObjectWithTag("GlobalFade").GetComponent<Image>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float distFromPlayer = Vector2.Distance(_playerController.transform.position, transform.position);
        float fadeAlpha = ((_objectWidth - distFromPlayer) + HardZoneDistanceFromCenter) / _objectWidth;
        Debug.Log(fadeAlpha);
        _globalFadeImage.color = new Color(0, 0, 0, Mathf.Clamp(fadeAlpha, 0, 1));
    }

}
