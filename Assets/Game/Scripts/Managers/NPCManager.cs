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
using Cinemachine;
public class NPCManager : Singleton<NPCManager>
{
    public List<NPCProps> mMissionNPCs;
    public List<NPCProps> mNormalNPCs;

    [HideInInspector] public readonly List<Transform> mPointsOfInterest = new List<Transform>();
    [HideInInspector] public readonly List<Transform> mBarLocation = new List<Transform>();
    [HideInInspector] public readonly List<int> mPoI = new List<int>();
    [HideInInspector] public readonly List<int> mBL = new List<int>();
    public GameMode mMode = GameMode.Bartender;
    [HideInInspector] public int mNPCsAtBar = 0;
    public int mMaxBarNPCs = 4;
    public int mMaxRaiseServer = 3;
    public float mGameStartDelay = 4;
    [HideInInspector] public int mRaiseServer = 0;
    [HideInInspector] public readonly List<CompanyNPC> mVisitedBar = new List<CompanyNPC>();
    [HideInInspector] public readonly List<CompanyNPC> mTotalNPCs = new List<CompanyNPC>();
    [HideInInspector] public readonly GameModeChangedEvent mGameModeChanged = new GameModeChangedEvent();
    [SerializeField] CinemachineVirtualCamera mBarCam;
    [SerializeField] CinemachineFreeLook mServerCam;
    [SerializeField] GameObject mServerObj;
    [SerializeField] GameObject mBartenderObj;
    void Update()
    {
        switch(mMode)
        {
            case GameMode.Bartender:
                if(mVisitedBar.Count == mTotalNPCs.Count)
                {
                    mMode = GameMode.Server;
                    mGameModeChanged.Invoke(mMode);
                    GameManager.Instance.mState = GameManager.State.NonGame;
                    MenuManager.Instance.ShowLoad();
                    mBarCam.Priority = 0;
                    mBartenderObj.SetActive(false);
                    mServerCam.Priority = 20;
                    mServerObj.SetActive(true);
                }
                break;
            case GameMode.Server:
                if(GameManager.Instance.mState == GameManager.State.NonGame)
                {
                    mGameStartDelay -= Time.deltaTime;
                    if(mGameStartDelay <= 0)
                    {
                        MenuManager.Instance.HideLoad();
                        GameManager.Instance.mState = GameManager.State.Game;
                    }
                }
                break;
        }
    }

    public NPCProps GetMissionNPC()
    {
        int aNPCIx = Random.Range(0, mMissionNPCs.Count);
        NPCProps aProp = mMissionNPCs[aNPCIx];
        aProp.mInebriationState = 0.0f;
        return aProp;
    }

    public NPCProps GetNormalNPC()
    {
        int aNPCIx = Random.Range(0, mNormalNPCs.Count);
        NPCProps aProp = mNormalNPCs[aNPCIx];
        aProp.mInebriationState = 0.0f;
        return aProp;
    }

}