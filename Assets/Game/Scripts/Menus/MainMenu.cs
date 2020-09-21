///-------------------------------------------------------------------------------------------------
// File: MainMenu.cs
//
// Author: Dakshvir Singh Rehill
// Date: 21/9/2020
//
// Summary:	This Menu is going to be the first menu that opens up in game
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
   public void StartGame()
    {
        GameManager.Instance.StartGame();
        MenuManager.Instance.HideMenu(mMenuClassifier);
    }
}