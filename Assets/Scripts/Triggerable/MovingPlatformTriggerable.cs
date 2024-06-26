using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Triggerable
{
    public float speed;
    List<Vector3> _controlPoints = new List<Vector3>();
    int currentControlPoint = 0;

    public bool isMoving = true;
    private bool _initialState;

    private void Awake()
    {
        _initialState = isMoving;
    }

    void Start()
    {
        Transform controlsPointsParent = transform.Find("ControlPoints");
        foreach (Transform child in controlsPointsParent.transform)
        {
            _controlPoints.Add(child.position);
        }
    }

    void FixedUpdate()
    {
        // If we're at the control point, move to the next one
        if (Vector3.Distance(transform.position, _controlPoints[currentControlPoint]) < 0.1f)
        {
            currentControlPoint++;
            if (currentControlPoint >= _controlPoints.Count)
            {
                currentControlPoint = 0;
            }
        }
        // Move towards the control point
        if (isMoving)
        {
            Vector3 direction = (_controlPoints[currentControlPoint] - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;

            // Move the passengers
            gameObject.TryGetComponent<Ridable>(out Ridable ridable);
            if (ridable != null)
            {
                ridable.MovePassengers(speed * Time.deltaTime * direction);
            }
        }
    }

    public void Reset()
    {
        isMoving = _initialState;
    }

    public override void Interact()
    {
        isMoving = !isMoving;
    }
}
