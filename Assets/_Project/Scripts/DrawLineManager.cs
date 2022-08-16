using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    public GameObject brush;
    public float distanceFromCam = 1f;
    public float drawThreshold = 0.01f;
    private Transform _camTransform;
    private LineRenderer _lineRendererBrush;
    private Vector3 _lastDrawPoint;
    private bool _isDrawing;

    void Start()
    {
        _camTransform = Camera.main.transform;
    }
    
    void Update()
    {
        if(_isDrawing)
        {
            Vector3 drawPoint = (_camTransform.forward * distanceFromCam) + _camTransform.position;
            
            if(_lineRendererBrush == null)
            {
                CreateBrush(drawPoint);
            }
            
            if (Vector3.Distance(drawPoint, _lastDrawPoint) > drawThreshold)
            {
                AddDrawPoint(drawPoint);
                _lastDrawPoint = drawPoint;
            }    
        }
    }
    
    [ContextMenu("Draw")]
    public void Draw()
    {
        _isDrawing = true;
    }
    
    [ContextMenu("Stop")]
    public void Stop()
    {
        _isDrawing = false;
        _lineRendererBrush = null;
    }
    
    private void CreateBrush(Vector3 drawPoint)
    {
        GameObject brushInstance = Instantiate(brush);
        _lineRendererBrush = brushInstance.GetComponent<LineRenderer>();
        _lineRendererBrush.SetPosition(0, drawPoint);
    }
    
    private void AddDrawPoint(Vector3 drawPoint)
    {
        _lineRendererBrush.positionCount++;
        int positionIndex = _lineRendererBrush.positionCount - 1;
        _lineRendererBrush.SetPosition(positionIndex, drawPoint);
    }
    
}
