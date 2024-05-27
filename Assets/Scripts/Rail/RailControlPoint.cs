using UnityEngine;

public class RailControlPoint : MonoBehaviour
{
    public bool isStop = false;

    void OnDrawGizmos()
    {
        Gizmos.color = isStop ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
