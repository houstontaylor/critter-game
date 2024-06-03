using System.Linq;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Rail : MonoBehaviour
{
   
    public Rail NextRail;
    public float RailMoveSpeed = 1.0f;

    private int currentControlPoint = 0;
    public GameObject _rider;
    private RailControlPoint[] _controlPoints;
    private LineRenderer _lineRenderer;  // This sprite's line renderer to show rails

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Start() {
        _controlPoints = transform.Find("ControlPoints").GetComponentsInChildren<RailControlPoint>();
        RenderRails();
        if (_rider != null) {
            Mount(_rider);
        }
    }

    public void Mount(GameObject newRider) {
        newRider.transform.position = _controlPoints[0].transform.position;
        newRider.transform.SetParent(null);
        _rider = newRider;
    }

    // Interpolate _rider towards next control point
    void FixedUpdate() {
        if (_rider == null) {
            return;
        }

        // First, check if we're at the control point, and increment if we are
        if (Vector3.Distance(_rider.transform.position, _controlPoints[currentControlPoint].transform.position) < 0.1f) {
            if (_controlPoints[currentControlPoint].isStop) {
                // Reaches a stop, stop moving until told to move again
                return;
            }
            _controlPoints[currentControlPoint].Trigger();
            currentControlPoint++;
            if (currentControlPoint >= _controlPoints.Length) {
                // Reached the end of the rail; dismount
                if (NextRail != null) {
                    NextRail.Mount(_rider);
                }
                _rider = null;
                return;
            }
        }

        // Then move nico towards the control point
        Vector3 direction = (_controlPoints[currentControlPoint].transform.position - _rider.transform.position).normalized;
        _rider.transform.position += RailMoveSpeed * Time.deltaTime * direction;
    }

    private void RenderRails()
    {
        _lineRenderer.positionCount = _controlPoints.Length;
        for (int i = 0; i < _controlPoints.Length; i++)
        {
            _lineRenderer.SetPosition(i, _controlPoints[i].transform.position);
        }
    }

    void OnDrawGizmos() {
        _controlPoints = transform.Find("ControlPoints").GetComponentsInChildren<RailControlPoint>();
        for (int i = 0; i < _controlPoints.Length - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_controlPoints[i].transform.position, _controlPoints[i+1].transform.position);
        }
    }

}
