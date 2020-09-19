///-------------------------------------------------------------------------------------------------
// File: AnimationCompletedSB.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary:	State Machine Behaviour used to call animation completed callback if listener is setup
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompletedSB : StateMachineBehaviour
{

	public override void OnStateExit(Animator pAnimator, AnimatorStateInfo pStateInfo, int pLayerIndex)
	{

		IAnimationCompleted aCompletedCallback = pAnimator.GetComponent<IAnimationCompleted>();
		if (aCompletedCallback != null)
		{
			aCompletedCallback.AnimationCompleted(pStateInfo.shortNameHash);
		}
	}
}
