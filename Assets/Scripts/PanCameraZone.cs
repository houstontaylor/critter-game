using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PanInfo
{
    public Transform TransformToPanTo;
    public float DelayBefore;
    public float AnimationTime;
}

public class PanCameraZone : MonoBehaviour
{

    [Header("Positions to Pan the Camera")]
    public List<PanInfo> PanPositions = new();
    public bool OnlyWorksOnce = true;

    private CinemachineVirtualCamera _mainCam;
    private PlayerMovement _playerMovement;

    private bool _alreadyTriggered = false;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        _mainCam = (CinemachineVirtualCamera)Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
    }

    /// <summary>
    /// When this save zone touches a player, saves the
    /// player's last recorded position.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnlyWorksOnce && _alreadyTriggered) { return; }
        if (collision.TryGetComponent(out PlayerController player))
        {
            _alreadyTriggered = true;
            StartCoroutine(PanCameraCoroutine());
        }
    }

    /// <summary>
    /// Pan the camera to all of the positions in the PanPositions list.
    /// Freezes the player movement at the beginning; frees it at the end.
    /// </summary>
    private IEnumerator PanCameraCoroutine()
    {
        _playerMovement.CanPlayerMove = false; 
        for (int i = 0; i < PanPositions.Count; i++)
        {
            yield return new WaitForSeconds(PanPositions[i].DelayBefore);
            _mainCam.Follow = PanPositions[i].TransformToPanTo;
            yield return new WaitForSeconds(PanPositions[i].AnimationTime);
        }
        _mainCam.Follow = _playerMovement.transform;
        _playerMovement.CanPlayerMove = true;
    }

    /// <summary>
    /// Draw the green boxes around save zones in the Scene view.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 colliderOffset = GetComponent<BoxCollider2D>().offset;
        Vector3 colliderHalfSize = GetComponent<BoxCollider2D>().size / 2;
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position - colliderHalfSize + colliderOffset, transform.position - new Vector3(-colliderHalfSize.x, colliderHalfSize.y) + colliderOffset);
        Gizmos.DrawLine(transform.position + colliderHalfSize + colliderOffset, transform.position - new Vector3(colliderHalfSize.x, -colliderHalfSize.y) + colliderOffset);

        Gizmos.color = Color.cyan;
        if (PanPositions.Count > 0)
        {
            Gizmos.DrawLine(transform.position, PanPositions[0].TransformToPanTo.position);
            for (int i = 1; i < PanPositions.Count - 1; i++)
            {
                Gizmos.DrawLine(PanPositions[i].TransformToPanTo.position, PanPositions[i + 1].TransformToPanTo.position);
            }
        }
    }

}
