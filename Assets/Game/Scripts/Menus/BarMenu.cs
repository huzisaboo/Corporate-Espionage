///-------------------------------------------------------------------------------------------------
// File: BarMenu.cs
//
// Author: Dakshvir Singh Rehill
// Date: 20/9/2020
//
// Summary:	This menu is going to be used to display the bar options to the player
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMenu : Menu
{
    [HideInInspector] public CompanyNPC mCurrentActiveClient;
    [SerializeField] List<Drink> mAllDrinks;
    public readonly Dictionary<DrinkBase, Drink> mDrinks = new Dictionary<DrinkBase, Drink>();

    public override void Start()
    {
        foreach (Drink aDrink in mAllDrinks)
        {
            if (mDrinks.ContainsKey(aDrink.m_drinkType))
            {
                continue;
            }
            mDrinks.Add(aDrink.m_drinkType, aDrink);
        }
        base.Start();
    }

}