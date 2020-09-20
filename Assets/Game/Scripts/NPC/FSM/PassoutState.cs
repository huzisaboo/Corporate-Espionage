///-------------------------------------------------------------------------------------------------
// File: PassoutState.cs
//
// Author: Dakshvir Singh Rehill
// Date: 20/9/2020
//
// Summary:	Passout State Machine when NPC passes out because of the drunkenness
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassoutState : BaseNPCState
{
    [SerializeField] string mBodyVertFloat = "BodyVert";
    int mBVHash = -1;

    public override void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        if(mCompanyNPC == null)
        {
            mBVHash = Animator.StringToHash(mBodyVertFloat);
        }
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        mCompanyNPC.mAnimator.SetLayerWeight(1, 1);
        mCompanyNPC.mAnimator.SetFloat(mBVHash, 1.0f);
    }
}