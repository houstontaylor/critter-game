using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ridable : MonoBehaviour
{
    List<GameObject> passengers = new List<GameObject>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.position.y > transform.position.y) {
            passengers.Add(collision.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        passengers.Remove(collision.gameObject);
    }

    public void MovePassengers(Vector3 delta)
    {
        foreach (GameObject passenger in passengers)
        {
            // Move the passenger
            passenger.transform.position += delta;

            // and its children
            passenger.TryGetComponent<Ridable>(out Ridable ridable);
            if (ridable != null)
            {
                ridable.MovePassengers(delta);
            }
        }
    }
}
