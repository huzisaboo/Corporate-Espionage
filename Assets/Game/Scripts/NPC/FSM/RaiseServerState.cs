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
        mCompanyNPC.mPlayerUI.gameObject.SetActive(true);
        mCompanyNPC.mPlayerUI.mTimerImage.gameObject.SetActive(true);
        mCompanyNPC.mPlayerUI.mDrinkImage.transform.parent.gameObject.SetActive(true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameManager.Instance.mState != GameManager.State.Game)
        {
            return;
        }
        if (mStateTriggered)
        {
            return;
        }
        mCurrentServerWaitTime += Time.deltaTime;
        mCompanyNPC.mPlayerUI.mTimerImage.fillAmount = (mCompanyNPC.mNPCProps.mServerWaitTime - mCurrentServerWaitTime) / mCompanyNPC.mNPCProps.mServerWaitTime;
        if (mCurrentServerWaitTime >= mCompanyNPC.mNPCProps.mServerWaitTime)
        {
            NPCManager.Instance.mRaiseServer--;
            MissionsManager.Instance.OnTimeOver(GameEndReason.Spotted);
        }
        else
        {
            if (Physics.OverlapSphere(mCompanyNPC.transform.position, mServerRadius, mServerMask).Length > 0)
            {
                animator.SetTrigger(mResetHash);
                NPCManager.Instance.mRaiseServer--;
                mCompanyNPC.mAnimator.SetTrigger(mServerEndHash);
                mStateTriggered = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mCompanyNPC.mPlayerUI.mDrinkImage.transform.parent.gameObject.SetActive(false);
        mCompanyNPC.mPlayerUI.mTimerImage.gameObject.SetActive(false);
        mCompanyNPC.mPlayerUI.gameObject.SetActive(false);
    }
}