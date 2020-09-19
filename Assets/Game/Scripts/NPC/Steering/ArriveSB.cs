///-------------------------------------------------------------------------------------------------
// File: ArriveSB.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary:	Steering Behavior that will make npc slow down when its about to reach
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveSB : SeekSB
{
    [SerializeField] float mSlowDownDistance = 5.0f;
    [SerializeField] float mDeceleration = 2.5f;
    [SerializeField] float mStoppingDistance = 0.5f;
    [HideInInspector] public bool mPathComplete = true;

    void Start()
    {
        mTarget = transform.position;
    }

    public override void CalculateNewPath(Vector3 pPathTarget)
    {
        mPathComplete = false;
        base.CalculateNewPath(pPathTarget);
    }

    public override Vector3 CalculateForce()
    {
        Vector3 aDistanceVector = mTarget - transform.position;

        float aMag = aDistanceVector.magnitude;

        if (aMag > mSlowDownDistance)
        {
            return base.CalculateForce();
        }
        else if (aMag < mStoppingDistance)
        {
            mPathComplete = true;
            return -mSteeringAgent.mVelocity;
        }

        float aSpeed = aMag / mDeceleration;
        if (aSpeed > mSteeringAgent.mMaxSpeed)
        {
            aSpeed = mSteeringAgent.mMaxSpeed;
        }

        aSpeed /= mSlowDownDistance;

        Vector3 aDesiredVelocity = aDistanceVector.normalized * aSpeed;

        return aDesiredVelocity - mSteeringAgent.mVelocity;
    }


    protected override void OnDrawGizmos()
    {
        if(!mDebugDraw)
        {
            return;
        }
        base.OnDrawGizmos();
        DebugExtension.DebugCircle(transform.position, Color.green, mSlowDownDistance);
    }

}