///-------------------------------------------------------------------------------------------------
// File: NPCManager.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary:	NPC Manager will have a pool of NPC information out of which NPCs will randomly pick
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public List<NPCProps> mMissionNPCs;
    public List<NPCProps> mNormalNPCs;

    public List<Transform> mPointsOfInterest;
    public List<Transform> mBarLocation;
    public GameMode mMode = GameMode.Bartender;
    [HideInInspector] public int mNPCsAtBar = 0;
    public int mMaxBarNPCs = 4;
    public int mMaxRaiseServer = 3;
    [HideInInspector] public int mRaiseServer = 0;
    public NPCProps GetMissionNPC()
    {
        int aNPCIx = Random.Range(0, mMissionNPCs.Count);
        NPCProps aProp = mMissionNPCs[aNPCIx];
        mMissionNPCs.RemoveAt(aNPCIx);
        return aProp;
    }

    public NPCProps GetNormalNPC()
    {
        int aNPCIx = Random.Range(0, mNormalNPCs.Count);
        NPCProps aProp = mNormalNPCs[aNPCIx];
        return aProp;
    }

}