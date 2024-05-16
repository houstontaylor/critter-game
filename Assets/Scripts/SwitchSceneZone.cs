using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneZone : MonoBehaviour
{

    [Header("Scene to Load when Touched")]
    public string TargetScene;

    /// <summary>
    /// When this switch scene zone is triggered, switch scene.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            SceneManager.LoadScene(TargetScene);
        }
    }

    /// <summary>
    /// Draw the red boxes around kill zones in the Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 colliderOffset = GetComponent<BoxCollider2D>().offset;
        Vector3 colliderHalfSize = GetComponent<BoxCollider2D>().size / 2;
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
    }

}
