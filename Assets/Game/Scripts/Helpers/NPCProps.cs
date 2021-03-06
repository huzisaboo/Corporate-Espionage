///-------------------------------------------------------------------------------------------------
// File: NPCProps.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/9/2020
//
// Summary:	All properties that will govern the NPC behavior in the game
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NPCProps", menuName = "NPC/NPC Properties")]
public class NPCProps : ScriptableObject
{
    public NPCType mNPCType;
    [Range(0.0f, 1.0f)] public float mDrunkThreshold;
    [Range(0.0f, 1.0f)] public float mPassoutThreshold;
    public float mBarWaitTime;
    public float mServerWaitTime;
    public int mMaxDrinksAtBar;
    public float mCorrectDrinkMultiplier;
    public float mIncorrectDrinkMultiplier;
    public List<DrinkBase> mPreferredDrinks;
    public Color mColor;
    //public NPCDepartment mNPCDepartment;
    //[HideInInspector]
    //public NPCMission mNPCMission;
}