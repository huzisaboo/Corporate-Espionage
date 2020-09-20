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

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mCompanyNPC == null)
        {
            mReachedHash = Animator.StringToHash(mReachedTrigger);
        }
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mCompanyNPC.mArriveBehavior.mPathComplete)
        {
            animator.SetTrigger(mReachedHash);
        }
    }
}