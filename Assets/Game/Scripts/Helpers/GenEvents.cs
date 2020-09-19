///-------------------------------------------------------------------------------------------------
// File: GenEvents.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary: Has Parameterized Unity Events defined
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event to provide list of strings on scene load or unload
/// </summary>
[Serializable]
public class SceneLoadedEvent : UnityEvent<List<string>> { }
/// <summary>
/// Event to call animation related events with int parameter
/// </summary>
[Serializable]
public class AnimationCompletedEvent : UnityEvent<int> { }

