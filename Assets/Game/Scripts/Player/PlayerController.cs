using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController m_controller;
    private Animator m_animator;
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovementUpdate();
    }

    private void MovementUpdate()
    {
        Vector3 movement = new Vector3(InputManager.Instance.GetXValue(), 0, InputManager.Instance.GetYValue());

        if (m_animator != null)
        {
            m_animator.SetFloat("Speed", movement.magnitude);
           if(movement != Vector3.zero)
            {
                transform.forward = movement;
            }
        }
    }
}

