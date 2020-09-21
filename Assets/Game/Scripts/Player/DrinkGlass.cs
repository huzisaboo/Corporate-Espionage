using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGlass : MonoBehaviour
{
    [SerializeField] GameObject liquid;
    private float m_alcoholPercentage = 0.0f;
    private MeshRenderer m_liquidMr;
    private Transform m_LiquidPvt;
    public DrinkBase mDrinkType;
    // Start is called before the first frame update
    void Start()
    {
        if (liquid != null)
        {
            m_liquidMr = liquid.GetComponent<MeshRenderer>();
            m_LiquidPvt = liquid.transform.parent;
        }
    }


    public void IncAlcoholPercentage(float p_value)
    {
        m_alcoholPercentage += p_value * 0.1f;
        m_alcoholPercentage = Mathf.Clamp(m_alcoholPercentage, 0, 1);
        if(m_LiquidPvt == null)
        {
            m_LiquidPvt = liquid.transform.parent;
        }
        m_LiquidPvt.localScale = new Vector3
            (m_LiquidPvt.localScale.x, m_alcoholPercentage, m_LiquidPvt.localScale.z);
    }

    public float GetAlcPercent()
    {
        return m_alcoholPercentage;
    }

    public void SetLiquidColor(Color p_color)
    {
        if (m_liquidMr == null)
        {
            if (liquid != null)
            {
                m_liquidMr = liquid.GetComponent<MeshRenderer>();
            }
        }
        if (m_liquidMr != null)
        {
            m_liquidMr.material.color = p_color;
        }
    }
}
