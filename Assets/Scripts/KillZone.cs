using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
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
        if (TryGetComponent(out BoxCollider2D box))
        {
            Vector3 colliderOffset = box.offset;
            Vector3 colliderHalfSize = box.size * transform.localScale / 2;
            Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
            Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
            Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
            Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
        } else if (TryGetComponent(out CircleCollider2D circ)) {
            Vector3 colliderOffset = circ.offset;
            float circRadius = circ.radius * transform.localScale.x;
            Gizmos.DrawWireSphere(transform.position + colliderOffset, circRadius);
        }
    }

}
