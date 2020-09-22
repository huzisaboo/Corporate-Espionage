///-------------------------------------------------------------------------------------------------
// File: CompanyNPC.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary:	NPC base class for all company NPCs
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyNPC : MonoBehaviour
{
    public NPCType mNPCMode;
    public Animator mAnimator;
    public AnimationListener mAnimListener;
    public SteeringAgent mAgent;
    public ArriveSB mArriveBehavior;
    public ObstacleAvoidanceSB mObstacleBehavior;
    public int mMaxIdleIx = 5;
    public SkinnedMeshRenderer mRenderer;
    public BarDrinkServe mPlayerUI;
    [HideInInspector] public NPCProps mNPCProps;
    [HideInInspector] public int mIdleIndex = 0;
    [HideInInspector] public bool mPassedOut = false;
    [HideInInspector] public DrinkGlass mGlass;
    public PlayerDialog mPlayerDialog;
    Material mMatCopy;
    public NPCMission mNPCMission;
    [HideInInspector]
    public int mTableIx = -1;
    [HideInInspector]
    public int mBarIx = -1;
    void Start()
    {
        NPCManager.Instance.mTotalNPCs.Add(this);
        mNPCProps = mNPCMode == NPCType.MissionNPC ? NPCManager.Instance.GetMissionNPC() :
            NPCManager.Instance.GetNormalNPC();
        NPCProps aProp = new NPCProps();
        aProp.mBarWaitTime = mNPCProps.mBarWaitTime;
        aProp.mColor = mNPCProps.mColor;
        aProp.mCorrectDrinkMultiplier = mNPCProps.mCorrectDrinkMultiplier;
        aProp.mDrunkThreshold = mNPCProps.mDrunkThreshold;
        aProp.mIncorrectDrinkMultiplier = mNPCProps.mIncorrectDrinkMultiplier;
        aProp.mInebriationState = mNPCProps.mInebriationState;
        aProp.mMaxDrinksAtBar = mNPCProps.mMaxDrinksAtBar;
        aProp.mNPCType = mNPCProps.mNPCType;
        aProp.mPassoutThreshold = mNPCProps.mPassoutThreshold;
        aProp.mPreferredDrinks = new List<DrinkBase>(mNPCProps.mPreferredDrinks);
        aProp.mServerWaitTime = mNPCProps.mServerWaitTime;
        mNPCProps = aProp;
        mMatCopy = mRenderer.material;
        mMatCopy.SetColor("_Color4", mNPCProps.mColor);
        mRenderer.material = mMatCopy;
        if(mNPCMode == NPCType.MissionNPC)
        {
            mPlayerDialog.symbolSprite = mNPCMission.missionBadge;
            foreach (PlayerMission mission in MissionsManager.Instance.mPlayerMissions)
            {
                if (mission.missionName == mNPCMission.missionName)
                {
                    mPlayerDialog.OnUpdateMissionProgress += mission.UpdateProgress;
                }
            }
        }
        else
        {
            mPlayerDialog.gameObject.SetActive(false);
        }
        NPCManager.Instance.mGameModeChanged.AddListener(SetDialogSymbol);
    }

    void OnDestroy()
    {
        if (NPCManager.IsValidSingleton())
        {
            NPCManager.Instance.mGameModeChanged.RemoveListener(SetDialogSymbol);
        }
    }

    void SetDialogSymbol(GameMode pMode)
    {
        mPlayerDialog.symbolDisplayFactor *= (1 - mNPCProps.mInebriationState);
    }

}