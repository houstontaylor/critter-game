using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : Triggerable
{
    [Header("Particle System Assignment")]
    public ParticleSystem _laserParticles;

    [Header("Laser Properties")]
    [SerializeField] private bool _isLaserActive = true;
    public bool IsLaserActive
    {
        get => _isLaserActive;
        set
        {
            _isLaserActive = value;
            UpdateLaser();  // Update the laser visual when this value is modified
        }
    }

    private LineRenderer _lineRenderer;
    private Vector3 _originalPosition;
    private float _originalAngle;
    private int _currentDeltaIndex = 0;
    private float _movementProgress = 0;

    [Header("Movement Properties")]
    public List<Vector3> movementDeltas;
    public float speed = 1.0f;
    public bool isMoving = false;

    [Header("Pivot Properties")]
    public float pivotSpeed = 30.0f; // degrees per second
    public bool isPivoting = false;
    public float pivotAngle = 45.0f; // Angle to pivot in both directions
    public bool pivotBetweenAngles = true; // If false, will rotate in a full circle

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _originalPosition = transform.position;
        _originalAngle = transform.rotation.eulerAngles.z;
        UpdateLaser();
    }

    public override void Interact()
    {
        IsLaserActive = !IsLaserActive;
    }

    private void Update()
    {
        if (!IsLaserActive) { return; }
        if (isMoving) MoveLaser();
        if (isPivoting) PivotLaser();
        UpdateLaser();
    }

    //private void MoveLaser()
    //{
    //    if (movementDeltas.Count == 0) return;

    //    // Calculate the progress based on speed and time
    //    _movementProgress += speed * Time.deltaTime;
    //    if (_movementProgress >= 1.0f)
    //    {
    //        _movementProgress = 0;
    //        _currentDeltaIndex = (_currentDeltaIndex + 1) % movementDeltas.Count;
    //    }

    //    // Interpolate between current and next position
    //    Vector3 startPosition = _originalPosition + movementDeltas[_currentDeltaIndex];
    //    Vector3 endPosition = _originalPosition + movementDeltas[(_currentDeltaIndex + 1) % movementDeltas.Count];
    //    transform.position = Vector3.Lerp(startPosition, endPosition, _movementProgress);
    //}

    private void MoveLaser()
    {
        if (movementDeltas.Count < 2) return; // Ensure there are at least two points to move between

        // Calculate the progress based on constant speed and time
        Vector3 startPosition = _originalPosition + movementDeltas[_currentDeltaIndex];
        Vector3 endPosition = _originalPosition + movementDeltas[(_currentDeltaIndex + 1) % movementDeltas.Count];
        float segmentDistance = Vector3.Distance(startPosition, endPosition);
        float segmentSpeed = speed / segmentDistance; // Adjust speed based on the segment distance

        _movementProgress += segmentSpeed * Time.deltaTime;
        if (_movementProgress >= 1.0f)
        {
            _movementProgress = 0; // Reset progress for the next segment
            _currentDeltaIndex = (_currentDeltaIndex + 1) % movementDeltas.Count; // Move to the next segment
        }
        else  // Else, or else we'll set movement to 0 and then instant TP the laser
        {

            // Interpolate between current and next position using the adjusted speed
            transform.position = Vector3.Lerp(startPosition, endPosition, _movementProgress);
        }
    }


    private void PivotLaser()
    {
        if (pivotBetweenAngles)
        {
            // Calculate rotation based on time and initial rotation angle
            float angle = Mathf.PingPong(Time.time * pivotSpeed, pivotAngle * 2) - pivotAngle;
            transform.rotation = Quaternion.Euler(0, 0, _originalAngle + angle);
        }
        else
        {
            transform.Rotate(Vector3.forward, pivotSpeed * Time.deltaTime);
        }
    }

    public void UpdateLaser()
    {
        if (!_isLaserActive)
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(new Vector3[] { });
            _laserParticles.Stop();
            return;
        }

        _lineRenderer.positionCount = 2;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 99, LayerMask.GetMask("Ground") | LayerMask.GetMask("Player"));
        if (hit)
        {
            _lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point + (Vector2)transform.right * 0.1f });
            _laserParticles.transform.position = hit.point;
            _laserParticles.Play();

            if (hit.transform.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.Respawn();
            }
        }
        else
        {
            Vector3 endPoint = transform.position + transform.right * 99;
            _lineRenderer.SetPositions(new Vector3[] { transform.position, endPoint });
        }
    }

    private void OnDrawGizmos()
    {
        if (IsLaserActive)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * 5);
        }
    }
}
