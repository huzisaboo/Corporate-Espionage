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
    public GameObject drinkGlassPrefab;
    public Transform glassSpawnPosition;
    [HideInInspector] public DrinkGlass drinkGlass;
    [HideInInspector] public CompanyNPC mCurrentActiveClient;
    [SerializeField] List<Drink> mAllDrinks;
    public GameObject mButtons;
    public GameObject mDrinkSelections;
    public readonly Dictionary<DrinkBase, Drink> mDrinks = new Dictionary<DrinkBase, Drink>();
    public readonly Dictionary<DrinkBase, DrinkMaker> mDrinkMakers = new Dictionary<DrinkBase, DrinkMaker>();
    [HideInInspector] public DrinkMaker mActiveDrinkMaker;
    [SerializeField] LongMouseClick mPourBtn;
    public override void Start()
    {
        base.Start();
        foreach (Drink aDrink in mAllDrinks)
        {
            if (mDrinks.ContainsKey(aDrink.m_drinkType))
            {
                continue;
            }
            mDrinks.Add(aDrink.m_drinkType, aDrink);
        }
    }

    void Update()
    {
        if(mActiveDrinkMaker != null && mPourBtn.IsButtonPressed())
        {
            mActiveDrinkMaker.ActivateDrinkMaker();
        }
    }

    public void ServeDrink()
    {
        mCurrentActiveClient.mGlass = drinkGlass;
        LeanTween.moveX(drinkGlass.gameObject, mCurrentActiveClient.transform.position.x, 0.2f);
        drinkGlass = null;
        mActiveDrinkMaker = null;
        MenuManager.Instance.HideMenu(mMenuClassifier);
        mButtons.SetActive(false);
        mDrinkSelections.SetActive(true);
    }
}