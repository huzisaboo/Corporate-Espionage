///-------------------------------------------------------------------------------------------------
// File: AnimationListener.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary:	Animation Listener allows other scripts to register to OnAnimatorMove 
// Animation event and also allows Animation Completed events to be fired.
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationListener : MonoBehaviour, IAnimationCompleted
{
	[HideInInspector] public readonly UnityEvent mOnAnimatorMove = new UnityEvent();
	[HideInInspector] public readonly AnimationCompletedEvent mOnAnimatorIK = new AnimationCompletedEvent();
	readonly Dictionary<int, AnimationCompletedEvent> mOnAnimationCompleted = new Dictionary<int, AnimationCompletedEvent>();
	void OnAnimatorMove()
	{
		mOnAnimatorMove.Invoke();
	}

	void OnAnimatorIK(int pLayerIndex)
	{
		mOnAnimatorIK.Invoke(pLayerIndex);
	}
	/// <summary>
	/// Used by State Behaviour to Call Completed Event
	/// </summary>
	/// <param name="pShortHashName"></param>
	public void AnimationCompleted(int pShortHashName)
	{
		AnimationCompletedEvent aEvent;
		if (mOnAnimationCompleted.TryGetValue(pShortHashName, out aEvent))
		{
			aEvent.Invoke(pShortHashName);
		}
	}

	/// <summary>
	/// Used to Register Animation Completed Callback
	/// </summary>
	/// <param name="pShortHashName">Anim Hash Value</param>
	/// <param name="pOnAnimationCompletedAction">Callback</param>
	public void RegisterOnAnimationCompleted(int pShortHashName, UnityAction<int> pOnAnimationCompletedAction)
	{
		AnimationCompletedEvent aEvent;
		if (!mOnAnimationCompleted.TryGetValue(pShortHashName, out aEvent))
		{
			aEvent = new AnimationCompletedEvent();
			mOnAnimationCompleted.Add(pShortHashName, aEvent);
		}
		aEvent.AddListener(pOnAnimationCompletedAction);
	}

	/// <summary>
	/// Used to Unregister Animation Completed Callback
	/// </summary>
	/// <param name="pShortHashName">Anim Hash Value</param>
	/// <param name="pOnAnimationCompletedAction">Callback</param>
	public void UnregisterOnAnimationCompleted(int pShortHashName, UnityAction<int> pOnAnimationCompletedAction)
	{
		AnimationCompletedEvent aEvent;
		if (mOnAnimationCompleted.TryGetValue(pShortHashName, out aEvent))
		{
			aEvent.RemoveListener(pOnAnimationCompletedAction);
		}
	}

}