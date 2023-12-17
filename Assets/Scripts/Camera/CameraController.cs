using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed, zoomInMin, zoomInMax, zoomOutLensSize;
    float targetLensSize;
    public CinemachineVirtualCamera cinemachineVirtualCamera => GetComponent<CinemachineVirtualCamera>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Zoom();
    }
    public void Zoom()
    {
        cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, targetLensSize, Time.deltaTime * zoomSpeed);
    }
    public void ZoomIn(float distance)
    {
        targetLensSize = Remap(distance, 0, 1, zoomInMax, zoomInMin);
    }
    public void ZoomOut()
    {
        targetLensSize = zoomOutLensSize;
    }
    float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
