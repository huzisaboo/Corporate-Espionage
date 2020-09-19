///-------------------------------------------------------------------------------------------------
// File: Singleton.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary: Base Singleton Class for all Singletons
///-------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T mInstance;

	public static T Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = (T)FindObjectOfType(typeof(T));
				if (mInstance == null)
				{
                    Debug.LogAssertionFormat("Singleton of type : {0} not in scene", typeof(T).Name);
				}
			}
			return mInstance;
		}
	}

	public static bool IsValidSingleton()
	{
		return (mInstance != null);
	}
}
