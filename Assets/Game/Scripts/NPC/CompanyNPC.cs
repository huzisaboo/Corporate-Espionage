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
    public int mIdleIndex = 0;

}