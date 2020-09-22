///-------------------------------------------------------------------------------------------------
// File: DecisionState.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary: Makes the decision to choose action of the player after staying idle for random time
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionState : BaseNPCState
{
    [SerializeField] string mIdleTrigger = "IdleSM";
    [SerializeField] string mNextTableTrigger = "TableSM";
    [SerializeField] string mGoToBarTrigger = "BarSM";
    [SerializeField] string mRaiseServerTrigger = "RaiseServer";
    int mIdleHash = -1;
    int mTableHash = -1;
    int mGoToBarHash = -1;
    int mRaiseServerHash = -1;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(mCompanyNPC == null)
        {
            mIdleHash = Animator.StringToHash(mIdleTrigger);
            mTableHash = Animator.StringToHash(mNextTableTrigger);
            mGoToBarHash = Animator.StringToHash(mGoToBarTrigger);
            mRaiseServerHash = Animator.StringToHash(mRaiseServerTrigger);
        }
        base.OnStateEnter(animator, stateInfo, layerIndex);
        bool aDecisionMade = false;
        switch(NPCManager.Instance.mMode)
        {
            case GameMode.Bartender:
                aDecisionMade = MakeDecisionForBar(animator);
                break;
            case GameMode.Server:
                aDecisionMade = MakeDecisionForFloor(animator);
                break;
        }
        if(!aDecisionMade)
        {
            float aRandomizer = Random.value;
            if(aRandomizer > 0.5f)
            {
                mCompanyNPC.mTableIx = MissionsManager.Instance.GetRandomIntExcluding
                    (NPCManager.Instance.mPointsOfInterest.Count, NPCManager.Instance.mPoI);
                NPCManager.Instance.mPoI.Add(mCompanyNPC.mTableIx);
                mCompanyNPC.mArriveBehavior.CalculateNewPath(NPCManager.Instance.mPointsOfInterest
                    [mCompanyNPC.mTableIx].position);
                animator.SetTrigger(mTableHash);
            }
            else
            {
                animator.SetTrigger(mIdleHash);
            }
        }
    }

    bool MakeDecisionForBar(Animator pFSM)
    {
        if(NPCManager.Instance.mVisitedBar.Contains(mCompanyNPC))
        {
            return false;
        }
        if(mCompanyNPC.mNPCProps.mInebriationState > mCompanyNPC.mNPCProps.mDrunkThreshold)
        {
            return false;
        }
        if(NPCManager.Instance.mNPCsAtBar >= NPCManager.Instance.mMaxBarNPCs)
        {
            return false;
        }
        NPCManager.Instance.mNPCsAtBar++;
        mCompanyNPC.mBarIx = MissionsManager.Instance.GetRandomIntExcluding(NPCManager.Instance.mBarLocation.Count, NPCManager.Instance.mBL);
        NPCManager.Instance.mBL.Add(mCompanyNPC.mBarIx);
        mCompanyNPC.mArriveBehavior.CalculateNewPath(NPCManager.Instance.mBarLocation[mCompanyNPC.mBarIx].position);
        pFSM.SetTrigger(mGoToBarHash);
        return true;
    }

    bool MakeDecisionForFloor(Animator pFSM)
    {
        if(NPCManager.Instance.mRaiseServer >= NPCManager.Instance.mMaxRaiseServer)
        {
            return false;
        }
        NPCManager.Instance.mRaiseServer++;
        pFSM.SetTrigger(mRaiseServerHash);
        return true;
    }

}