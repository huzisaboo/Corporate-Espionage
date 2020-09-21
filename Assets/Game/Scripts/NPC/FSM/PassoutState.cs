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
    int mBVHash = -1;
    [SerializeField] float mPassoutTime = 4.0f;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameManager.Instance.mState != GameManager.State.Game)
        {
            return;
        }
        mPassoutTime -= Time.deltaTime;
        if(mPassoutTime <= 0.0f)
        {
            mCompanyNPC.gameObject.SetActive(false);
        }
    }
}