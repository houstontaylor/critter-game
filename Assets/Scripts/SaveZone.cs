using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour
{

    [Header("Area to Respawn the Player")]
    public Transform RespawnPosition; 

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
    }

}
