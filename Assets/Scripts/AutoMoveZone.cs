using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AutoMoveZone : MonoBehaviour
{

    [Header("Horizontal Distance to Cover (+ = right, - = left)")]
    public float horDist;

    /// <summary>
    /// When this save zone touches a player, saves the
    /// player's last recorded position.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement pMovement))
        {
            StartCoroutine(MovePlayerToPositionCoroutine(pMovement));
        }
    }

    private IEnumerator MovePlayerToPositionCoroutine(PlayerMovement pMovement)
    {
        pMovement.CanPlayerMove = false;
        Vector3 destPosition = pMovement.transform.position + new Vector3(horDist, 0);
        while (Vector3.Distance(pMovement.transform.position, destPosition) > 0.5f)
        {
            pMovement.MovePlayerHorizontally(1);
            yield return null;
        }
        pMovement.CanPlayerMove = true;
    }

    /// <summary>
    /// Draw the yellow boxes around save zones in the Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 colliderOffset = GetComponent<BoxCollider2D>().offset;
        Vector3 colliderHalfSize = GetComponent<BoxCollider2D>().size / 2;
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);

        Gizmos.DrawLine(transform.position + new Vector3(0, 0.1f, 0), transform.position + new Vector3(horDist, 0.1f, 0));
        Gizmos.DrawLine(transform.position + new Vector3(0, -0.1f, 0), transform.position + new Vector3(horDist, -0.1f, 0));
    }

}
