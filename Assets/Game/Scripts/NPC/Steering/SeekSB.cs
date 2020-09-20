///-------------------------------------------------------------------------------------------------
// File: SeekSB.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary:	Seek Steering Behavior for NPC movement
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSB : SteeringBehaviourBase
{
	public override Vector3 CalculateForce()
	{
		Vector3 aDesiredVelocity = (mTarget - transform.parent.position).normalized;
		aDesiredVelocity = aDesiredVelocity * mSteeringAgent.mMaxSpeed;
		return aDesiredVelocity - mSteeringAgent.mVelocity;
	}

	protected virtual void OnDrawGizmos()
	{
		if(!mDebugDraw)
        {
			return;
        }
		DebugExtension.DebugWireSphere(mTarget, Color.red);
	}
}