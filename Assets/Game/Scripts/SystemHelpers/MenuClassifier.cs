///-------------------------------------------------------------------------------------------------
// File: MenuClassifier.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary: Scriptable OBject used to classify each UI menu
///-------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuClassifier", menuName = "UI/Menu Classifier")]
public class MenuClassifier : ScriptableObject
{
	public string mMenuName;
}
