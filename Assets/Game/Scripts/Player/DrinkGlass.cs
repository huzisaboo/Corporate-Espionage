using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGlass : MonoBehaviour
{
    public GameObject liquid;

    private float m_alcoholPercentage = 0.0f;
    private MeshRenderer m_liquidMr;
    // Start is called before the first frame update
    void Start()
    {
        if (liquid != null)
        {
            m_liquidMr = liquid.GetComponent<MeshRenderer>();
        }
    }


    public void IncAlcoholPercentage(float p_value)
    {
        Debug.Log(p_value);
        m_alcoholPercentage += p_value;
    }

    public void SetLiquidColor(Color p_color)
    {
        if(m_liquidMr == null)
        {
            if (liquid != null)
            {
                m_liquidMr = liquid.GetComponent<MeshRenderer>();
            }
        }
        if(m_liquidMr!=null)
        {
            m_liquidMr.material.color = p_color;
        }
    }
}
