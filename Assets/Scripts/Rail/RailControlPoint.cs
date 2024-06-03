using UnityEngine;

public class RailControlPoint : MonoBehaviour
{
    public bool isStop = false;
    public Cutscene cutsceneToTrigger;
    private bool cutsceneTriggered;
    public DoorTriggerable doorToTrigger;
    private bool doorTriggered;

    void OnDrawGizmos()
    {
        Gizmos.color = isStop ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    public void Continue() {
        isStop = false;
    }
    
    // Called when Nico reaches a control point. If the
    // point is a stop, this is only called after Nico continues past it.
    public void Trigger() {
        if (doorToTrigger != null && !doorTriggered)
        {
            doorTriggered = true;
            doorToTrigger.Interact();
        }
        if (cutsceneToTrigger != null && !cutsceneTriggered)
        {
            cutsceneTriggered = true;
            cutsceneToTrigger.Play();
        }
    }
}
