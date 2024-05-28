using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorTriggerable : Triggerable
{
    List<GameObject> passengers = new List<GameObject>();
    public bool isActive = true;
    public float speed = 1.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        passengers.Add(collision.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        passengers.Remove(collision.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            // Move the passengers
            // FIXME: Stacking when a passenger is on multiple conveyors
            gameObject.TryGetComponent<Ridable>(out Ridable ridable);
            if (ridable != null)
            {
                ridable.MovePassengers(speed * Time.deltaTime * transform.right);
            }
        }
    }

    public override void Interact()
    {
        isActive = !isActive;
    }
}
