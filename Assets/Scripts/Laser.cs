using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{

    private LineRenderer _lineRenderer;  // To edit the line renderer to shoot the laser

    [SerializeField] private bool _isLaserActive = true;
    public bool IsLaserActive
    {
        get => _isLaserActive;
        set
        {
            _isLaserActive = value;
            UpdateLaserVisual();  // Update the laser visual when this value is modified
        }
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Should be called any time this laser should be updated.
    /// Automatically called any time the laser's active boolean is toggled.
    /// (If you do something like move the laser, this should be called.)
    /// 
    /// If the laser is inactive, does not render the laser.
    /// </summary>
    public void UpdateLaserVisual()
    {
        if (IsLaserActive) {
            _lineRenderer.SetPositions(null);
            return; 
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 99, LayerMask.GetMask("Ground"));
        if (hit)
        {
            Vector3[] positions = { transform.position, hit.point + (Vector2)transform.right };
            _lineRenderer.SetPositions(positions);
        }
        else
        {
            // If no hit, set the end point far in the direction
            Vector3 endPoint = transform.position + transform.right * 99;
            Vector3[] positions = { transform.position, endPoint };
            _lineRenderer.SetPositions(positions);
        }
    }

    /// <summary>
    /// Draw the direction of the laser.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (IsLaserActive)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 5);
        }
    }

}
