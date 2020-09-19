///-------------------------------------------------------------------------------------------------
// File: SteeringBehaviourBase.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary:	Each steering behaviour will use this as base for virtual function
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviourBase : MonoBehaviour
{
	public float mWeight = 1.0f;
	protected Vector3 mTarget;
	[SerializeField] protected bool mDebugDraw = false;
	public abstract Vector3 CalculateForce();
	[HideInInspector] public SteeringAgent mSteeringAgent;

	public virtual void CalculateNewPath(Vector3 pPathTarget)
	{
		mTarget = pPathTarget;
	}
}