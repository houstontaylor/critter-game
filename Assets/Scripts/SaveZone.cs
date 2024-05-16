using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour
{

    [Header("Area to Respawn the Player")]
    public Transform RespawnPosition;

    [Header("(Optional) Objects to Reset On Death")]
    public GameObject[] ObjectsToReset;

    private List<Vector3> _savedObjectPositions = new();

    private PlayerController _playerController;

    /// <summary>
    /// Save all of the object positions to reset to.
    /// </summary>
    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        foreach (GameObject obj in ObjectsToReset)
        {
            _savedObjectPositions.Add(obj.transform.position);
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
        }
    }

    /// <summary>
    /// Reset all objects in the scene.
    /// </summary>
    private void ResetObjects()
    {
        for (int i = 0; i < ObjectsToReset.Length; i++)
        {
            ObjectsToReset[i].transform.position = _savedObjectPositions[i];
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

        Gizmos.color = Color.cyan;
        foreach (GameObject obj in ObjectsToReset)
        {
            Gizmos.DrawLine(transform.position, obj.transform.position);
        }
    }

}
