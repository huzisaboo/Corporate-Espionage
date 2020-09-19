///-------------------------------------------------------------------------------------------------
// File: PoolClassifier.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary: Used to classify and store object pools
///-------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolClassifier", menuName = "Optimization/Pool Classifier")]
public class PoolClassifier : ScriptableObject
{
	public string mPoolName;
}
