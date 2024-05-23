using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public Vector3 LastRecordedSpawnLocation;  // Where the player will respawn on death

    public Action OnDeath = null;

    private Rigidbody2D _rb2D;

    public GameObject holding;

    public void PickUp(GameObject item) {
        if (holding != null) {
            Drop();
        }
        holding = item;
        item.transform.SetParent(transform);
    }

    public void Drop() {
        if (holding == null) {
            return;
        }
        holding.transform.SetParent(null);
        holding = null;
    }

    public void ClearPickup() {
        Destroy(holding);
        holding = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if there's something in interaction range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
            // Filter for interactibles
            Collider2D[] interactables = Array.FindAll(colliders, collider => 
                collider.TryGetComponent<Interactable>(out Interactable interactable) && 
                interactable.IsInteractable && 
                interactable.gameObject != holding // Ignore held items
            );
            // Find nearest
            Array.Sort(interactables, (a, b) => 
                Vector2.Distance(a.transform.position, transform.position)
                .CompareTo(Vector2.Distance(b.transform.position, transform.position))
            );
            Interactable nearest = interactables.Length > 0 ? interactables[0].GetComponent<Interactable>() : null;

            if (nearest != null)
            {
                nearest.Interact();
            } else {
                Drop();
            }
        }
    }

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        LastRecordedSpawnLocation = transform.position;  // Spawn location is where the player starts, initially
    }

    /// <summary>
    /// Teleports player to last recorded spawn location when called.
    /// </summary>
    public void Respawn()
    {
        transform.position = LastRecordedSpawnLocation;
        _rb2D.velocity = Vector3.zero;
        if (OnDeath != null)
        {
            OnDeath.Invoke();
        }
    }

    /// <summary>
    /// Saves the player's spawn location to somewhere else.
    /// </summary>
    public void SavePosition(Vector3 position)
    {
        LastRecordedSpawnLocation = position;
    }

}
