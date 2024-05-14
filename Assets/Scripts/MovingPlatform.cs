using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;
    List<Vector3> controlPoints = new List<Vector3>();
    int currentControlPoint = 0;

    void Start()
    {
        Transform controlsPointsParent = transform.Find("ControlPoints");
        foreach (Transform child in controlsPointsParent.transform)
        {
            controlPoints.Add(child.position);
        }
    }

    // Update is called once per frame
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
        transform.position = Vector3.MoveTowards(transform.position, controlPoints[currentControlPoint], speed * Time.deltaTime);
    }
}
