///-------------------------------------------------------------------------------------------------
// File: BodyTipsyControl.cs
//
// Author: Dakshvir Singh Rehill
// Date: 20/9/2020
//
// Summary:	Controls the movement of player when they get tipsy
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTipsyControl : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] float mMaxTipsyVal = 0.6f;
    [Range(0, 1)] [SerializeField] float mMinTipsyVal = 0.25f;
    [SerializeField] CompanyNPC mNPC;
    float mCurrentTipsyVal = 0.0f;
    [SerializeField] string mBodyHorFloat = "BodyHor";
    [SerializeField] string mBodyVertFloat = "BodyVert";
    [SerializeField] float mAnimatorDelay = 1.0f;
    float mCurDelay = 0.0f;
    int mBodyHorHash;
    int mBodyVertHash;

    void Start()
    {
        mBodyHorHash = Animator.StringToHash(mBodyHorFloat);
        mBodyVertHash = Animator.StringToHash(mBodyVertFloat);
    }

    void Update()
    {
        if(mNPC.mPassedOut)
        {
            return;
        }
        mCurDelay -= Time.deltaTime;
        if(mCurDelay > 0.0f)
        {
            return;
        }
        mCurDelay = mAnimatorDelay;
        mCurrentTipsyVal = mNPC.mNPCProps.mInebriationState * (mMaxTipsyVal - mMinTipsyVal);
        float aTipsyHor = Random.Range(-mCurrentTipsyVal, mCurrentTipsyVal);
        float aTipsyVer = Random.Range(-mCurrentTipsyVal, mCurrentTipsyVal);
        mNPC.mAnimator.SetFloat(mBodyHorHash, aTipsyHor);
        mNPC.mAnimator.SetFloat(mBodyVertHash, aTipsyVer);
        mNPC.mAnimator.SetLayerWeight(1, mNPC.mNPCProps.mInebriationState);
    }

}