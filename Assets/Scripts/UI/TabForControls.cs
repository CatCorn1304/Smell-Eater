using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabForControls : MonoBehaviour
{
    public GameObject controls;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
                controls.SetActive(true);
        }
    }
}
