using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KillZone : MonoBehaviour
{

    /// <summary>
    /// When this kill zone touches a player, call the
    /// Respawn() method on that player.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.Respawn();
        }
    }

    /// <summary>
    /// Draw the red boxes around kill zones in the Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 colliderOffset = GetComponent<BoxCollider2D>().offset;
        Vector3 colliderHalfSize = GetComponent<BoxCollider2D>().size / 2;
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
    }

}
