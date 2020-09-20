///-------------------------------------------------------------------------------------------------
// File: IdleState.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/9/2020
//
// Summary:	State of the NPC when it is idle
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseNPCState
{
    [SerializeField] float mMinIdleTime = 2.0f;
    [SerializeField] float mMaxIdleTime = 5.0f;
    float mCurrentIdleTime;
    float mIdleStayTime;
    int mDecideHash = -1;
    [SerializeField] string mDecideActionStateTrigger = "DecideAction";
    [SerializeField] string mIdleBase = "Idle";
    int mCurIdleHash = -1;
    bool mTriggerSet = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mCompanyNPC == null)
        {
            mDecideHash = Animator.StringToHash(mDecideActionStateTrigger);
        }
        base.OnStateEnter(animator, stateInfo, layerIndex);
        mIdleStayTime = Random.Range(mMinIdleTime, mMaxIdleTime);
        mCurrentIdleTime = 0.0f;
        SetIdleState(-1);
        mTriggerSet = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mTriggerSet)
        {
            return;
        }
        //wait for given idle time before going to resolve state
        if (mCurrentIdleTime < mIdleStayTime)
        {
            mCurrentIdleTime += Time.deltaTime;
            return;
        }
        mTriggerSet = true;
        animator.SetTrigger(mDecideHash);
    }

    void SetIdleState(int pIx)
    {
        mCompanyNPC.mIdleIndex = Random.Range(0, mCompanyNPC.mMaxIdleIx);
        mCurIdleHash = Animator.StringToHash(mIdleBase + mCompanyNPC.mIdleIndex);
        mCompanyNPC.mAnimListener.RegisterOnAnimationCompleted(mCurIdleHash, SetIdleState);
        mCompanyNPC.mAnimator.SetInteger("IdleIx", mCompanyNPC.mIdleIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mCompanyNPC.mAnimListener.UnregisterOnAnimationCompleted(mCurIdleHash, SetIdleState);
    }

}