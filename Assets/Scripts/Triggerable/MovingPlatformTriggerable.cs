using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Triggerable
{
    public float speed = 1.0f;
    List<Vector3> controlPoints = new List<Vector3>();
    int currentControlPoint = 0;

    public bool isMoving = true;

    void Start()
    {
        Transform controlsPointsParent = transform.Find("ControlPoints");
        foreach (Transform child in controlsPointsParent.transform)
        {
            controlPoints.Add(child.position);
        }
    }

    void FixedUpdate()
    {
        // If we're at the control point, move to the next one
        if (Vector3.Distance(transform.position, controlPoints[currentControlPoint]) < 0.1f)
        {
            currentControlPoint++;
            if (currentControlPoint >= controlPoints.Count)
            {
                currentControlPoint = 0;
            }
        }
        // Move towards the control point
        if (isMoving)
        {
            Vector3 direction = (controlPoints[currentControlPoint] - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;

            // Move the passengers
            gameObject.TryGetComponent<Ridable>(out Ridable ridable);
            if (ridable != null)
            {
                ridable.MovePassengers(speed * Time.deltaTime * direction);
            }
        }
    }

    public override void Interact()
    {
        isMoving = !isMoving;
    }
}
