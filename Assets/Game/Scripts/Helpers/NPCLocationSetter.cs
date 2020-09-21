///-------------------------------------------------------------------------------------------------
// File: NPCLocationSetter.cs
//
// Author: Dakshvir Singh Rehill
// Date: 21/9/2020
//
// Summary:	Sets the location list in the NPC Manager
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLocationSetter : MonoBehaviour
{
    [System.Serializable]
    public enum LocationType
    {
        Bar,
        Table
    }

    [SerializeField] LocationType mType;

    void Start()
    {
        if (!NPCManager.IsValidSingleton())
        {
            return;
        }
        List<Transform> aLocations = new List<Transform>();
        for (int aI = 0; aI < transform.childCount; aI++)
        {
            aLocations.Add(transform.GetChild(aI));
        }
        switch (mType)
        {
            case LocationType.Bar:
                NPCManager.Instance.mBarLocation.AddRange(aLocations);
                break;
            case LocationType.Table:
                NPCManager.Instance.mPointsOfInterest.AddRange(aLocations);
                break;
        }
    }
}