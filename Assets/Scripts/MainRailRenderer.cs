using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MainRailRenderer : MonoBehaviour
{

    private LineRenderer _lineRenderer;  // This sprite's line renderer to show rails
    private GameObject[] _controlPoints;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _controlPoints = GameObject.FindGameObjectsWithTag("RailCP");
        RenderRails();  // Render rails ONCE at the beginning of the game
    }

   private void RenderRails()
    {
        _lineRenderer.positionCount = _controlPoints.Length;
        for (int i = 0; i < _controlPoints.Length; i++)
        {
            _lineRenderer.SetPosition(i, _controlPoints[i].transform.position);
        }
    }

}
