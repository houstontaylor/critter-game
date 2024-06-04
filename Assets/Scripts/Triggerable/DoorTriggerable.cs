using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerable : Triggerable
{

    [Header("Door Properties")]
    public Vector3 PositionToMove;
    public float SecondsToAnimate;

    [Header("Object Assignments")]
    [SerializeField] private ParticleSystem _pSystem;

    public override void Interact()
    {
        StartCoroutine(MoveToPositionCoroutine(SecondsToAnimate));
    }

    /// <summary>
    /// Move to the relative position specified by `PositionToMove`
    /// over a provided number of seconds.
    /// </summary>
    private IEnumerator MoveToPositionCoroutine(float secs)  // lol sex
    {
        _pSystem.Emit(6);
        Vector3 startPos = transform.position;
        Vector3 destPos = startPos + PositionToMove;
        float currTime = 0;
        while (currTime < secs)
        {
            transform.position = Vector3.Lerp(startPos, destPos, Mathf.SmoothStep(0, 1, currTime / secs));
            currTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + PositionToMove);
        Gizmos.DrawWireSphere(transform.position + PositionToMove, 0.5f);
    }

}
