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
    [HideInInspector] public float mInebriationState = 0.0f;
    public NPCType mNPCType;
    [Range(0.0f, 1.0f)] public float mDrunkThreshold;
    [Range(0.0f, 1.0f)] public float mPassoutThreshold;
    public List<DrinkBase> mPreferredDrinks;
    public Color mColor;
}