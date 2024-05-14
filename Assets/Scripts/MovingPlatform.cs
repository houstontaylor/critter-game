using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    List<Vector3> controlPoints = new List<Vector3>();
    int currentControlPoint = 0;

    List<GameObject> passengers = new List<GameObject>();

    void Start()
    {
        Transform controlsPointsParent = transform.Find("ControlPoints");
        foreach (Transform child in controlsPointsParent.transform)
        {
            controlPoints.Add(child.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        passengers.Add(collision.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        passengers.Remove(collision.gameObject);
    }

    void Update()
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
        Vector3 direction = (controlPoints[currentControlPoint] - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;
        foreach (GameObject passenger in passengers)
        {
            passenger.transform.position += speed * Time.deltaTime * direction;
        }
    }
}
