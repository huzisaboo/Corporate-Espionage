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
    [HideInInspector] public float mCurrentDrinkAmnt = 0;
    [HideInInspector] public bool mPassedOut = false;
    Material mMatCopy;
    void Start()
    {
        NPCManager.Instance.mTotalNPCs.Add(this);
        mNPCProps = mNPCMode == NPCType.MissionNPC ? NPCManager.Instance.GetMissionNPC() :
            NPCManager.Instance.GetNormalNPC();
        mMatCopy = mRenderer.material;
        mMatCopy.SetColor("_Color4", mNPCProps.mColor);
        mRenderer.material = mMatCopy;
    }
}