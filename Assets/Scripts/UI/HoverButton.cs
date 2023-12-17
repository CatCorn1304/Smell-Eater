using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject earUp;
    public GameObject earDown;

    public void OnPointerEnter(PointerEventData eventData)
    {
        earUp.SetActive(true);
        earDown.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        earUp.SetActive(false);
        earDown.SetActive(true);
    }
}
