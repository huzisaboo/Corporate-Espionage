using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private float m_movementX;
    private float m_movementY;

    private void Update()
    {
        AxisUpdate();
    }

    private void AxisUpdate()
    {
        m_movementX = Input.GetAxis("Horizontal");
        m_movementY = Input.GetAxis("Vertical");
    }

    public float GetXValue()
    {
        return m_movementX;
    }

    public float GetYValue()
    {
        return m_movementY;
    }
}
