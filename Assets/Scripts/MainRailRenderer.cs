using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MainRailRenderer : MonoBehaviour
{

    private LineRenderer _lineRenderer;  // This sprite's line renderer to show rails
    private List<GameObject> _controlPoints;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _controlPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("RailCP"));
        _controlPoints.Sort((x, y) => (int)(y.transform.position.x - x.transform.position.x));
        RenderRails();  // Render rails ONCE at the beginning of the game
    }

   private void RenderRails()
    {
        _lineRenderer.positionCount = _controlPoints.Count;
        for (int i = 0; i < _controlPoints.Count; i++)
        {
            _lineRenderer.SetPosition(i, _controlPoints[i].transform.position);
        }
    }

}
