///-------------------------------------------------------------------------------------------------
// File: BaseNPCState.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary:	NPC State Machine's basic state
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPCState : StateMachineBehaviour
{
    protected CompanyNPC mCompanyNPC;
    
    public override void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        if(mCompanyNPC == null)
        {
            mCompanyNPC = pFSM.transform.parent.gameObject.GetComponent<CompanyNPC>();
        }
    }
}