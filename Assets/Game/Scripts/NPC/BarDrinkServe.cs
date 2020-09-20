﻿///-------------------------------------------------------------------------------------------------
// File: BarDrinkServe.cs
//
// Author: Dakshvir Singh Rehill
// Date: 20/9/2020
//
// Summary:	Script to show the timer and the type of drink to the bartender and also call for server
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarDrinkServe : MonoBehaviour
{
    public Image mTimerImage;
    public Image mDrinkImage;

    void Start()
    {
        mDrinkImage.color = Color.white;
        mTimerImage.gameObject.SetActive(false);
        mDrinkImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}