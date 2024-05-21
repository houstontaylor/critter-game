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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
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
