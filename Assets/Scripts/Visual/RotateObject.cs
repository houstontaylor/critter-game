using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 200.0f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate around the Z-axis at the specified speed
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
