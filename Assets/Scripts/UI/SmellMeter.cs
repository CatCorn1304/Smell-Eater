using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmellMeter : MonoBehaviour
{
    public CameraController cc;
    public Image smellMeterFill;
    public SniffController sniffScript;
    bool sniffing;

    void Start()
    {
    }
    private void Update()
    {
        sniffing = sniffScript.sniffing;
        if (sniffing)
        {
            float distancePercentage = Remap(sniffScript.closestDistance, 0, sniffScript.maxDistance, sniffScript.maxDistance, 0) / sniffScript.maxDistance;
            if (cc != null)
            {
                cc.ZoomIn(distancePercentage);
            }
            smellMeterFill.fillAmount = distancePercentage;
        }
        else
        {
            if (cc != null)
            {
                cc.ZoomOut();
            }
        }
        if (smellMeterFill.fillAmount > 0)
        {
            smellMeterFill.fillAmount -= Time.deltaTime;
        }
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
