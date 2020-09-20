///-------------------------------------------------------------------------------------------------
// File: BarState.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/9/2020
//
// Summary: NPC Functionality at Bar
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarState : BaseNPCState
{
    int mDrinksAtBar = 0;
    float mCurrentWaitTime = 0;
    [SerializeField] int mWaitingIdleIx;
    [SerializeField] int mAttendedIdleIx;
    [SerializeField] string mPassoutTrigger = "Passout";
    int mPassoutHash = -1;
    [SerializeField] string mIdleBase = "Idle";
    int mCurIdleHash = -1;
    [SerializeField] string mNextTableTrigger = "TableSM";
    int mTableHash = -1;
    [SerializeField] string mDrinkTrigger = "Drink";
    [SerializeField] float mRaycastDist = 50.0f;
    [SerializeField] LayerMask mRayMask;
    int mDrinkHash = -1;
    InternalBarState mNPCState = InternalBarState.Waiting;
    bool mDrinkTriggered = false;
    [System.Serializable]
    public enum InternalBarState
    {
        Waiting,
        Attended,
        Drinking
    }

    bool mStateTriggered = false;

    public override void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        if (mCompanyNPC == null)
        {
            mDrinkHash = Animator.StringToHash(mDrinkTrigger);
            mTableHash = Animator.StringToHash(mNextTableTrigger);
            mPassoutHash = Animator.StringToHash(mPassoutTrigger);
        }
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        mCurrentWaitTime = 0.0f;
        mDrinksAtBar = 0;
        mDrinkTriggered = false;
        mNPCState = InternalBarState.Waiting;
        SetIdleState(-1);
        mStateTriggered = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(mStateTriggered)
        {
            return;
        }
        switch (mNPCState)
        {
            case InternalBarState.Waiting:
                NPCWaiting();
                break;
            case InternalBarState.Attended:
                NPCAttended();
                break;
            case InternalBarState.Drinking:
                NPCDrinking();
                break;
        }
    }

    void NPCWaiting()
    {
        mCurrentWaitTime += Time.deltaTime;
        if (mCurrentWaitTime >= mCompanyNPC.mNPCProps.mBarWaitTime)
        {
            GoToTable();
        }
        else
        {
            //if menu is still visible return
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit aHitInfo;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out aHitInfo, mRaycastDist,mRayMask))
                {
                    if(aHitInfo.collider.gameObject.GetInstanceID() == mCompanyNPC.gameObject.GetInstanceID())
                    {
                        //open menu from menu manager
                        mNPCState = InternalBarState.Attended;
                    }
                }
            }
        }
    }

    void GoToTable()
    {
        mCompanyNPC.mArriveBehavior.CalculateNewPath(NPCManager.Instance.mPointsOfInterest
        [Random.Range(0, NPCManager.Instance.mPointsOfInterest.Count)].position);
        mFSM.SetTrigger(mTableHash);
        mStateTriggered = true;
    }

    void NPCAttended()
    {
        //check if menu is still visible
        //if not then go to drinking state
    }

    void NPCDrinking()
    {
        if(!mDrinkTriggered)
        {
            mDrinkTriggered = true;
            mDrinksAtBar++;
            mCompanyNPC.mAnimator.SetTrigger(mDrinkHash);
            mCompanyNPC.mAnimListener.RegisterOnAnimationCompleted(mDrinkHash, AnalyzeDrinking);
        }
    }

    void AnalyzeDrinking(int pIx)
    {
        mCompanyNPC.mAnimListener.UnregisterOnAnimationCompleted(mDrinkHash, AnalyzeDrinking);
        mCompanyNPC.mNPCProps.mInebriationState += mCompanyNPC.mCurrentDrinkAmnt;
        mCompanyNPC.mCurrentDrinkAmnt = 0.0f;
        mCompanyNPC.mPassedOut = mCompanyNPC.mNPCProps.mInebriationState >= mCompanyNPC.mNPCProps.mPassoutThreshold;
        if (mCompanyNPC.mPassedOut)
        {
            mFSM.SetTrigger(mPassoutHash);
            mStateTriggered = true;
        }
        else if (mDrinksAtBar >= mCompanyNPC.mNPCProps.mMaxDrinksAtBar)
        {
            GoToTable();
        }
        else
        {
            mCurrentWaitTime = 0.0f;
            mNPCState = InternalBarState.Waiting;
            mDrinkTriggered = false;
        }
    }

    void SetIdleState(int pIx)
    {
        mCompanyNPC.mIdleIndex =
            mNPCState == InternalBarState.Waiting
            && mCompanyNPC.mIdleIndex != mWaitingIdleIx
            ? mWaitingIdleIx : mAttendedIdleIx;
        mCurIdleHash = Animator.StringToHash(mIdleBase + mCompanyNPC.mIdleIndex);
        mCompanyNPC.mAnimListener.RegisterOnAnimationCompleted(mCurIdleHash, SetIdleState);
        mCompanyNPC.mAnimator.SetInteger("IdleIx", mCompanyNPC.mIdleIndex);
    }


}