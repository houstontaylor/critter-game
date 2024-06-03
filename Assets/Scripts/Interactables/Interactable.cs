using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{

    [Header("(Optional) Popup Assignment")]
    public GameObject PopupObject;
    [HideInInspector] public bool IsInteractable = false;  //  Can be checked by other scripts
                                                           //  to see if this is interactable

    private void Awake()
    {
        if (PopupObject != null)
        {
            PopupObject.SetActive(false);
        }
    }

    /// <summary>
    /// If we collide with the player, show a popup.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ShouldShowPopup()) { return;  }
        if (PopupObject != null && collision.gameObject.CompareTag("Player"))
        {
            ShowPopup();
        }
    }

    public void ShowPopup()
    {
        IsInteractable = true;
        PopupObject.SetActive(true);
    }

    /// <summary>
    /// If we stop colliding with the player, hide the popup.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsInteractable = false;
        if (PopupObject != null && collision.gameObject.CompareTag("Player"))
        {
            PopupObject.SetActive(false);
        }
    }
    public virtual bool ShouldShowPopup() {
        // FIXME: multiple pickupables near each other show multiple popups
        return true;
    }

    public abstract void Interact();
}
