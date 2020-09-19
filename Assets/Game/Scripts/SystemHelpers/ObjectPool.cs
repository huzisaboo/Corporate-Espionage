///-------------------------------------------------------------------------------------------------
// File: Object Pool.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary: Consists of information about each Pool Object that needs to be in the scene
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public PoolClassifier mPoolID;
    public GameObject mPoolObject;
    public int mPoolCapacity;
    public int mPoolIncreaseAmount;

    void Awake()
    {
        ObjectPoolingManager.Instance.AddObjectPool(this);
    }

    void OnDestroy()
    {
        if(!ObjectPoolingManager.IsValidSingleton())
        {
            return;
        }
        ObjectPoolingManager.Instance.RemoveObjectPool(mPoolID);
    }

}
