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
    public float DelayAfter;
}

public class PanCameraZone : MonoBehaviour
{

    [Header("Positions to Pan the Camera")]
    public List<PanInfo> PanPositions = new();
    public bool OnlyWorksOnce = true;

    private CinemachineVirtualCamera _mainVCam;
    private Camera _mainCam;
    private PlayerMovement _playerMovement;

    private bool _alreadyTriggered = false;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
        _mainVCam = (CinemachineVirtualCamera)_mainCam.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        // FIXME: _mainVCam is null when scene is loaded from another scene
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
        // For some reason, if the _mainVCam is null, then try assigning it again
        // (Removing this causes an issue when a new scene is loaded)
        if (_mainVCam == null)
        {
            _mainVCam = (CinemachineVirtualCamera)_mainCam.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        }
        Vector3 startPos = _playerMovement.transform.position;
        startPos.y += 1.2f;  // This is hard coded to match the Cinemachine's Y offset
        startPos.z = -10;
        _mainVCam.enabled = false;
        _playerMovement.CanPlayerMove = false;
        Vector3 currPos;
        float currTime, timeToWait;
        for (int i = 0; i < PanPositions.Count; i++)
        {
            yield return new WaitForSeconds(PanPositions[i].DelayBefore);
            currTime = 0;
            timeToWait = PanPositions[i].AnimationTime;
            currPos = _mainCam.transform.position;
            while (currTime < timeToWait)
            {
                Vector3 midArea = Vector3.Lerp(currPos, PanPositions[i].TransformToPanTo.position, Mathf.SmoothStep(0, 1, currTime / timeToWait));
                midArea.z = -10;
                _mainCam.transform.position = midArea;
                currTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(PanPositions[i].DelayAfter);
        }
        currTime = 0;
        timeToWait = 1;
        currPos = _mainCam.transform.position;
        while (currTime < timeToWait)
        {
            _mainCam.transform.position = Vector3.Lerp(currPos, startPos, Mathf.SmoothStep(0, 1, currTime / timeToWait));
            currTime += Time.deltaTime;
            yield return null;
        }
        _playerMovement.CanPlayerMove = true;
        _mainVCam.enabled = true;
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
