///-------------------------------------------------------------------------------------------------
// File: ObstacleAvoidanceSB.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/09/2020
//
// Summary:	Used to Avoid Obstacles in the NPC path
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceSB : SteeringBehaviourBase
{
    [System.Serializable]
    public struct Feeler
    {
        public float mDistance;
        public Vector3 mOffset;
    }

    [SerializeField] List<Feeler> mFeelers;

    [SerializeField] LayerMask mObstacleLayers;

    public override Vector3 CalculateForce()
    {
        RaycastHit aHit;
        Vector3 aFinalForce = Vector3.zero;
        foreach (Feeler aFeeler in mFeelers)
        {
            Vector3 aFPos = transform.rotation * aFeeler.mOffset + transform.position;
            if (Physics.Raycast(new Ray(aFPos, transform.forward), out aHit, aFeeler.mDistance, mObstacleLayers))
            {
                Vector3 aColliderPosition = aHit.collider.transform.position;
                Vector3 aCollisionPoint = Vector3.Project(aColliderPosition - transform.position, transform.forward) + transform.position;
                float aAvoidanceStrength = 1.0f + (aCollisionPoint.magnitude - aFeeler.mDistance) / aFeeler.mDistance;
                aFinalForce += (aCollisionPoint - aColliderPosition).normalized * aAvoidanceStrength;
            }
        }
        return aFinalForce;
    }


    void OnDrawGizmos()
    {
        if(!mDebugDraw)
        {
            return;
        }
        foreach (Feeler aFeeler in mFeelers)
        {
            Vector3 aFPos = transform.rotation * aFeeler.mOffset + transform.position;
            Debug.DrawLine(aFPos, aFPos + transform.forward * aFeeler.mDistance, Color.blue);
        }
    }
}