using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceImage : MonoBehaviour
{
    public float speed = 1f; // Speed of the bouncing motion
    public float height = 1f; // Height of the bounce

    [HideInInspector] public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the vertical offset using a sine wave
        float yOffset = Mathf.Sin(Time.time * speed) * height;

        // Set the new position of the object
        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
    }
}
