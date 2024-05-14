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
    void Update()
    {
        if (isActive)
        {
            // Move all passengers in the belt direction
            foreach (GameObject passenger in passengers)
            {
                passenger.transform.position += speed * Time.deltaTime * transform.right;
            }
        }
    }

    public override void Interact()
    {
        isActive = !isActive;
    }
}
