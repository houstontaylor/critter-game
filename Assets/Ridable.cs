using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ridable : MonoBehaviour
{
    List<GameObject> passengers = new List<GameObject>();
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

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

    void FixedUpdate()
    {
        foreach (GameObject passenger in passengers)
        {
            passenger.transform.position += transform.position - lastPosition;
        }
        lastPosition = transform.position;
    }
}
