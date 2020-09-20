using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Player/Drink")]
public class Drink : ScriptableObject
{
    public DrinkBase m_drinkType;

    public float m_concentration;

    public Color m_drinkColor;
}
