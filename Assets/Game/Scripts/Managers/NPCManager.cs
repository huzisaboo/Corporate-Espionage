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
using UnityEngine.Events;
public class NPCManager : Singleton<NPCManager>
{
    public List<NPCProps> mMissionNPCs;
    public List<NPCProps> mNormalNPCs;

    [HideInInspector] public List<Transform> mPointsOfInterest;
    [HideInInspector] public List<Transform> mBarLocation;
    public GameMode mMode = GameMode.Bartender;
    [HideInInspector] public int mNPCsAtBar = 0;
    public int mMaxBarNPCs = 4;
    public int mMaxRaiseServer = 3;
    [HideInInspector] public int mRaiseServer = 0;
    [HideInInspector] public readonly List<CompanyNPC> mVisitedBar = new List<CompanyNPC>();
    [HideInInspector] public readonly List<CompanyNPC> mTotalNPCs = new List<CompanyNPC>();
    [HideInInspector] public readonly GameModeChangedEvent mGameModeChanged = new GameModeChangedEvent();

    void Update()
    {
        switch(mMode)
        {
            case GameMode.Bartender:
                if(mVisitedBar.Count == mTotalNPCs.Count)
                {
                    //change game mode
                    mMode = GameMode.Server;
                    mGameModeChanged.Invoke(mMode);
                }
                break;
            case GameMode.Server:
                break;
        }
    }

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