using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ClickToSwitchScene : MonoBehaviour, IPointerDownHandler
{
    public string Scene;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
        SceneManager.LoadScene(Scene);
    }
}
