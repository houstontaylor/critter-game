//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
//public class Laser : Triggerable
//{

//    [Header("Particle System Assignment")]
//    public ParticleSystem _laserParticles;

//    [Header("Laser Properties")]
//    [SerializeField] private bool _isLaserActive = true;
//    public bool IsLaserActive
//    {
//        get => _isLaserActive;
//        set
//        {
//            _isLaserActive = value;
//            UpdateLaser();  // Update the laser visual when this value is modified
//        }
//    }

//    private LineRenderer _lineRenderer;  // To edit the line renderer to shoot the laser

//    private void Awake()
//    {
//        _lineRenderer = GetComponent<LineRenderer>();
//        UpdateLaser();
//    }

//    /// <summary>
//    /// Toggles the laser from the on to off state, or vice versa.
//    /// </summary>
//    public override void Interact()
//    {
//        IsLaserActive = !IsLaserActive;
//    }

//    private void Update()
//    {
//        if (!IsLaserActive) { return; }
//        UpdateLaser();  // This will be unperformant but it works!
//    }

//    /// <summary>
//    /// Should be called any time this laser should be updated.
//    /// Automatically called any time the laser's active boolean is toggled.
//    /// (If you do something like move the laser, this should be called.)
//    /// 
//    /// If the laser is inactive, does not render the laser.
//    /// </summary>
//    public void UpdateLaser()
//    {
//        // If the laser is inactive, set the positions to an empty list
//        if (!_isLaserActive) {
//            _lineRenderer.positionCount = 0;
//            _lineRenderer.SetPositions(new Vector3[] { });
//            _laserParticles.Stop();
//            return; 
//        }
//        // Or else, try to render a hit, and shoot a laser off in that direction
//        _lineRenderer.positionCount = 2;
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 99, LayerMask.GetMask("Ground") | LayerMask.GetMask("Player"));
//        if (hit)
//        {
//            _lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point + (Vector2)transform.right * 0.1f });
//            _laserParticles.transform.position = hit.point;
//            _laserParticles.Play();

//            // Make the player respawn if the laser raycasts hits them.
//            if (hit.transform.gameObject.TryGetComponent(out PlayerController playerController)) {
//                playerController.Respawn();
//            }
//        }
//        else
//        {
//            // If no hit, set the end point far in the direction
//            Vector3 endPoint = transform.position + transform.right * 99;
//            _lineRenderer.SetPositions(new Vector3[] { transform.position, endPoint });
//        }
//    }

//    /// <summary>
//    /// Draw the direction of the laser.
//    /// </summary>
//    private void OnDrawGizmos()
//    {
//        if (IsLaserActive)
//        {
//            Gizmos.color = Color.yellow;
//            Gizmos.DrawLine(transform.position, transform.position + transform.right * 5);
//        }
//    }

//}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
//public class Laser : Triggerable
//{
//    [Header("Particle System Assignment")]
//    public ParticleSystem _laserParticles;

//    [Header("Laser Properties")]
//    [SerializeField] private bool _isLaserActive = true;
//    public bool IsLaserActive
//    {
//        get => _isLaserActive;
//        set
//        {
//            _isLaserActive = value;
//            UpdateLaser();  // Update the laser visual when this value is modified
//        }
//    }

//    private LineRenderer _lineRenderer;  // To edit the line renderer to shoot the laser

//    [Header("Rotation Properties")]
//    public float rotationSpeed = 0f; // Speed of rotation
//    public float rotationAngle = 0f; // Max angle of rotation

//    private float initialRotationAngle;

//    private void Awake()
//    {
//        _lineRenderer = GetComponent<LineRenderer>();
//        UpdateLaser();

//        // Store the initial rotation angle
//        initialRotationAngle = transform.rotation.eulerAngles.z;
//    }

//    /// <summary>
//    /// Toggles the laser from the on to off state, or vice versa.
//    /// </summary>
//    public override void Interact()
//    {
//        IsLaserActive = !IsLaserActive;
//    }

//    private void Update()
//    {
//        if (!IsLaserActive) { return; }

//        // Calculate rotation based on time and initial rotation angle
//        float angle = Mathf.PingPong(Time.time * rotationSpeed, rotationAngle * 2) - rotationAngle;
//        transform.rotation = Quaternion.Euler(0, 0, initialRotationAngle + angle);

//        UpdateLaser();  // This will be unperformant but it works!
//    }

//    /// <summary>
//    /// Should be called any time this laser should be updated.
//    /// Automatically called any time the laser's active boolean is toggled.
//    /// (If you do something like move the laser, this should be called.)
//    /// 
//    /// If the laser is inactive, does not render the laser.
//    /// </summary>
//    public void UpdateLaser()
//    {
//        // If the laser is inactive, set the positions to an empty list
//        if (!_isLaserActive)
//        {
//            _lineRenderer.positionCount = 0;
//            _lineRenderer.SetPositions(new Vector3[] { });
//            _laserParticles.Stop();
//            return;
//        }
//        // Or else, try to render a hit, and shoot a laser off in that direction
//        _lineRenderer.positionCount = 2;
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 99, LayerMask.GetMask("Ground") | LayerMask.GetMask("Player"));
//        if (hit)
//        {
//            _lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point + (Vector2)transform.right * 0.1f });
//            _laserParticles.transform.position = hit.point;
//            _laserParticles.Play();

//            // Make the player respawn if the laser raycasts hits them.
//            if (hit.transform.gameObject.TryGetComponent(out PlayerController playerController))
//            {
//                playerController.Respawn();
//            }
//        }
//        else
//        {
//            // If no hit, set the end point far in the direction
//            Vector3 endPoint = transform.position + transform.right * 99;
//            _lineRenderer.SetPositions(new Vector3[] { transform.position, endPoint });
//        }
//    }

//    /// <summary>
//    /// Draw the direction of the laser.
//    /// </summary>
//    private void OnDrawGizmos()
//    {
//        if (IsLaserActive)
//        {
//            Gizmos.color = Color.yellow;
//            Gizmos.DrawLine(transform.position, transform.position + transform.right * 5);
//        }
//    }
//}

using System.Collections;
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

    private LineRenderer _lineRenderer;  // To edit the line renderer to shoot the laser

    [Header("Rotation Properties")]
    public float rotationSpeed = 30f; // Speed of rotation
    public float rotationAngle = 45f; // Max angle of rotation

    [Header("Movement Properties")]
    [SerializeField] private bool _isMoving = false; // Default is not moving
    public bool IsMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }

    private float initialRotationAngle;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        UpdateLaser();

        // Store the initial rotation angle
        initialRotationAngle = transform.rotation.eulerAngles.z;
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

        if (IsMoving)
        {
            // Calculate rotation based on time and initial rotation angle
            float angle = Mathf.PingPong(Time.time * rotationSpeed, rotationAngle * 2) - rotationAngle;
            transform.rotation = Quaternion.Euler(0, 0, initialRotationAngle + angle);
        }

        UpdateLaser();  // This will be unperformant but it works!
    }

    /// <summary>
    /// Should be called any time this laser should be updated.
    /// Automatically called any time the laser's active boolean is toggled.
    /// (If you do something like move the laser, this should be called.)
    /// 
    /// If the laser is inactive, does not render the laser.
    /// </summary>
    public void UpdateLaser()
    {
        // If the laser is inactive, set the positions to an empty list
        if (!_isLaserActive)
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(new Vector3[] { });
            _laserParticles.Stop();
            return;
        }
        // Or else, try to render a hit, and shoot a laser off in that direction
        _lineRenderer.positionCount = 2;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 99, LayerMask.GetMask("Ground") | LayerMask.GetMask("Player"));
        if (hit)
        {
            _lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point + (Vector2)transform.right * 0.1f });
            _laserParticles.transform.position = hit.point;
            _laserParticles.Play();

            // Make the player respawn if the laser raycasts hits them.
            if (hit.transform.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.Respawn();
            }
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