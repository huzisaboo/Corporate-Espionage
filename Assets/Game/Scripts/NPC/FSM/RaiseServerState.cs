///-------------------------------------------------------------------------------------------------
// File: RaiseServerState.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/9/2020
//
// Summary:	Provides functionality to wait for sometime and then get suspicious about player
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseServerState : BaseNPCState
{
    float mCurrentServerWaitTime = 0.0f;
    [SerializeField] float mServerRadius = 5.0f;
    [SerializeField] LayerMask mServerMask;
    [SerializeField] string mResetTrigger = "Reached";
    [SerializeField] string mRaiseServerTrigger = "RaiseServer";
    [SerializeField] string mServerEndTrigger = "ServerEnd";
    int mResetHash = -1;
    int mRaiseServerHash = -1;
    int mServerEndHash = -1;
    bool mStateTriggered = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mCompanyNPC == null)
        {
            mResetHash = Animator.StringToHash(mResetTrigger);
            mRaiseServerHash = Animator.StringToHash(mRaiseServerTrigger);
            mServerEndHash = Animator.StringToHash(mServerEndTrigger);
        }
        base.OnStateEnter(animator, stateInfo, layerIndex);
        mCurrentServerWaitTime = 0.0f;
        mStateTriggered = false;
        mCompanyNPC.mAnimator.SetTrigger(mRaiseServerHash);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mStateTriggered)
        {
            return;
        }
        mCurrentServerWaitTime += Time.deltaTime;
        if (mCurrentServerWaitTime >= mCompanyNPC.mNPCProps.mServerWaitTime)
        {
            //game over
        }
        else
        {
            if (Physics.OverlapSphere(mCompanyNPC.transform.position, mServerRadius, mServerMask).Length > 0)
            {
                animator.SetTrigger(mResetHash);
                mCompanyNPC.mAnimator.SetTrigger(mServerEndHash);
                mStateTriggered = true;
            }
        }
    }
}