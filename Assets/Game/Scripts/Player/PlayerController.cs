using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator m_animator;
    private Vector3 m_velocity;
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovementUpdate();
    }

    private void MovementUpdate()
    { 
        Vector3 a_cameraForward = Camera.main.transform.forward;
        Vector3 a_cameraRight = Camera.main.transform.right;

        Vector3 a_translation = InputManager.Instance.GetYValue() * a_cameraForward;
        a_translation += InputManager.Instance.GetXValue() * a_cameraRight;
        a_translation.y = 0;        //Since camera can point itself upwards and downwards, we do not want the character to orient itself in that direction so ignoring Y values
        if (a_translation.magnitude > 0)
        {
            m_velocity = a_translation;
            transform.forward = m_velocity;
            if(m_animator != null)
            {
                m_animator.SetFloat("Speed", m_velocity.magnitude);
            }
        }
    }
}

