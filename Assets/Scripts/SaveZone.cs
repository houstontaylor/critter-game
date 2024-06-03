using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour
{

    [Header("Area to Respawn the Player")]
    public Transform RespawnPosition;
    
    [Header("(Optional) Rail Control Point to Trigger")]
    public RailControlPoint railControlPoint;

    [Header("(Optional) Objects to Reset On Death")]
    public GameObject[] ObjectsToReset;

    private Dictionary<int, Vector3> _savedPositions = new();

    private PlayerController _playerController;

    /// <summary>
    /// Save all of the object positions to reset to.
    /// </summary>
    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        for (int i = 0; i < ObjectsToReset.Length; i++)
        {
            GameObject obj = ObjectsToReset[i];
            _savedPositions[i] = obj.transform.position;
        }
    }

    private void Start()
    {
        if (_playerController != null)
        {
            _playerController.OnDeath += ResetObjects;  // Reset objects when the player dies
        }
    }

    /// <summary>
    /// When this save zone touches a player, saves the
    /// player's last recorded position.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.SavePosition(RespawnPosition.position);
            if (railControlPoint != null)
            {
                railControlPoint.Continue();
            }
        }
    }

    /// <summary>
    /// Reset all objects in the scene.
    /// </summary>
    private void ResetObjects()
    {
        for (int i = 0; i < ObjectsToReset.Length; i++)
        {
            ObjectsToReset[i].transform.position = _savedPositions[i];
            if (ObjectsToReset[i].TryGetComponent(out Laser laser))
            {
                laser.Reset();
            }
            if (ObjectsToReset[i].TryGetComponent(out WireInteractable wire))
            {
                wire.Reset();
            }
            if (ObjectsToReset[i].TryGetComponent(out MovingPlatform movPlat))
            {
                movPlat.Reset();
            }
        }
    }

    /// <summary>
    /// Draw the green boxes around save zones in the Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 colliderOffset = GetComponent<BoxCollider2D>().offset;
        Vector3 colliderHalfSize = GetComponent<BoxCollider2D>().size / 2;
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);

        foreach (GameObject obj in ObjectsToReset)
        {
            Gizmos.DrawLine(transform.position, obj.transform.position);
        }
        
        // Connect to the rail control point
        if (railControlPoint != null)
        {
            Gizmos.color = new Color(1f, 0.25f, 0.25f);
            Gizmos.DrawLine(transform.position, railControlPoint.transform.position);
        }
    }

}
