using UnityEngine;

public class Rail : MonoBehaviour
{
    public RailControlPoint[] controlPoints;
    public Rail nextRail;
    int currentControlPoint = 0;
    public float speed = 1.0f;
    GameObject rider;
    void Start() {
        controlPoints = transform.Find("ControlPoints").GetComponentsInChildren<RailControlPoint>();
    }

    public void Mount(GameObject newRider) {
        newRider.transform.position = controlPoints[0].transform.position;
        newRider.transform.SetParent(null);
        rider = newRider;
    }

    // Interpolate rider towards next control point
    void FixedUpdate() {
        if (rider == null) {
            return;
        }

        // First, check if we're at the control point, and increment if we are
        if (Vector3.Distance(rider.transform.position, controlPoints[currentControlPoint].transform.position) < 0.1f) {
            if (controlPoints[currentControlPoint].isStop) {
                // Reaches a stop, stop moving until told to move again
                return;
            }
            currentControlPoint++;
            if (currentControlPoint >= controlPoints.Length) {
                // Reached the end of the rail; dismount
                if (nextRail != null) {
                    nextRail.Mount(rider);
                }
                rider = null;
                return;
            }
        }

        // Then move nico towards the control point
        Vector3 direction = (controlPoints[currentControlPoint].transform.position - rider.transform.position).normalized;
        rider.transform.position += speed * Time.deltaTime * direction;
    }
}
