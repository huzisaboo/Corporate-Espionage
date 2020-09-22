///-------------------------------------------------------------------------------------------------
// File: DrunkParticleChanger.cs
//
// Author: Dakshvir Singh Rehill
// Date: 20/9/2020
//
// Summary:	Changes the particle effect according to inebriation
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkParticleChanger : MonoBehaviour
{
    [SerializeField] float mMinParticles = 0;
    [SerializeField] float mMaxParticles = 50;
    [SerializeField] ParticleSystem mSystem;
    [SerializeField] CompanyNPC mNPC;

    ParticleSystem.EmissionModule mEmissionModule;

    void Start()
    {
        mEmissionModule = mSystem.emission;
    }

    void Update()
    {
        mEmissionModule.rateOverTime = new ParticleSystem.
            MinMaxCurve(mNPC.mInebriationState * (mMaxParticles - mMinParticles));
    }
}