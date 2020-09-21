///-------------------------------------------------------------------------------------------------
// File: MoveToGoal.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/9/2020
//
// Summary:	Will check if npc reached goal and change fsm state
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGoal : BaseNPCState
{
    [SerializeField] string mReachedTrigger = "Reached";
    int mReachedHash = -1;
    bool mStateTriggered = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mCompanyNPC == null)
        {
            mReachedHash = Animator.StringToHash(mReachedTrigger);
        }
        base.OnStateEnter(animator, stateInfo, layerIndex);
        mStateTriggered = false;
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
        if (mCompanyNPC.mArriveBehavior.mPathComplete)
        {
            animator.SetTrigger(mReachedHash);
            mStateTriggered = true;
        }
    }
}