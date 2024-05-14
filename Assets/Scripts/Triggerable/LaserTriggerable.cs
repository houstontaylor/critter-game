using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : Triggerable
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
        UpdateLaserVisual();
    }

    /// <summary>
    /// Toggles the laser from the on to off state, or vice versa.
    /// </summary>
    public override void Interact()
    {
        IsLaserActive = !IsLaserActive;
    }

    private void Update()
    {
        if (!IsLaserActive) { return; }
        // Make the player respawn if the laser raycasts hits them.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 99);
        if (hit && hit.transform.gameObject.TryGetComponent(out PlayerController playerController))
        {
            playerController.Respawn();
        }
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
        // If the laser is inactive, set the positions to an empty list
        if (!_isLaserActive) {
            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(new Vector3[] { });
            return; 
        }
        // Or else, try to render a hit, and shoot a laser off in that direction
        _lineRenderer.positionCount = 2;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 99, LayerMask.GetMask("Ground"));
        if (hit)
        {
            _lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point + (Vector2)transform.right });
        }
        else
        {
            // If no hit, set the end point far in the direction
            Vector3 endPoint = transform.position + transform.right * 99;
            _lineRenderer.SetPositions(new Vector3[] { transform.position, endPoint });
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
