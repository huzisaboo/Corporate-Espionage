using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LongMouseClick : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private bool m_pointerDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_pointerDown = false;
    }

    public bool IsButtonPressed()
    {
        return m_pointerDown;
    }
}
