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
    public override void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        //play passout anim
    }
}