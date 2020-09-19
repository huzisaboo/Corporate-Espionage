///-------------------------------------------------------------------------------------------------
// File: GameManager.cs
//
// Author: Dakshvir Singh Rehill
// Date: 22/6/2020
//
// Summary:	Manager responsible for the game's transition i.e. circle of life
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Awake()
    {
        LeanTween.init(1000);
    }

    void Start()
    {
        MultiSceneManager.Instance.mOnSceneLoad.AddListener(OnSceneLoad);
        MultiSceneManager.Instance.mOnSceneUnload.AddListener(OnSceneUnload);
    }

    void OnDestroy()
    {
        if (MultiSceneManager.IsValidSingleton())
        {
            MultiSceneManager.Instance.mOnSceneLoad.RemoveListener(OnSceneLoad);
            MultiSceneManager.Instance.mOnSceneUnload.RemoveListener(OnSceneUnload);
        }
    }


    void OnSceneLoad(List<string> pLoadedScenes)
    {
    }

    void OnSceneUnload(List<string> pUnloadedScenes)
    {
    }


}
