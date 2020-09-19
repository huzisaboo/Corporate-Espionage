﻿///-------------------------------------------------------------------------------------------------
// File: MultiSceneManager.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary: Maintains and manages additive scene transitions from one scene to another
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiSceneManager : Singleton<MultiSceneManager>
{
    [HideInInspector] public readonly SceneLoadedEvent mOnSceneLoad = new SceneLoadedEvent();
    [HideInInspector] public readonly SceneLoadedEvent mOnSceneUnload = new SceneLoadedEvent();
    public float mDelayTime = 1.0f;
    public readonly List<string> mCurrentLoadedScenes = new List<string>();

    void Awake()
    {
        mCurrentLoadedScenes.Add(SceneManager.GetActiveScene().path);
    }

    public void LoadScene(string pSceneName)
    {
        List<string> aTempList = new List<string>();
        aTempList.Add(pSceneName);
        StartCoroutine(LoadMultipleScenesWithDelay(aTempList));
    }

    public void LoadScenes(List<string> pSceneNames)
    {
        StartCoroutine(LoadMultipleScenesWithDelay(pSceneNames));
    }

    public void UnloadScene(string pSceneName)
    {
        List<string> aTempList = new List<string>();
        aTempList.Add(pSceneName);
        StartCoroutine(UnloadMultipleScenes(aTempList));
    }

    public void UnloadScenes(List<string> pSceneNames)
    {
        StartCoroutine(UnloadMultipleScenes(pSceneNames));
    }

    IEnumerator UnloadMultipleScenes(List<string> pSceneNames)
    {
        if (MenuManager.IsValidSingleton())
        {
            MenuManager.Instance.ShowLoad();
        }

        foreach (string aScene in pSceneNames)
        {
            yield return StartCoroutine(AsyncSceneUnload(aScene));
            mCurrentLoadedScenes.Remove(aScene);
        }

        mOnSceneUnload.Invoke(pSceneNames);
        if (MenuManager.IsValidSingleton())
        {
            MenuManager.Instance.HideLoad();
        }
    }

    IEnumerator AsyncSceneUnload(string pSceneName)
    {
        if (!mCurrentLoadedScenes.Contains(pSceneName))
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(mDelayTime);
            Application.backgroundLoadingPriority = ThreadPriority.Low;

            AsyncOperation aSync = SceneManager.UnloadSceneAsync(pSceneName);
            yield return new WaitUntil(() => aSync.isDone);

            aSync = Resources.UnloadUnusedAssets();
            yield return new WaitUntil(() => aSync.isDone);
            Application.backgroundLoadingPriority = ThreadPriority.Normal;
            yield return new WaitForSeconds(mDelayTime);
        }

    }

    IEnumerator LoadMultipleScenesWithDelay(List<string> pSceneNames)
    {
        MenuManager.Instance.ShowLoad();
        foreach (string aSceneName in pSceneNames)
        {
            yield return StartCoroutine(LoadSceneWithDelay(aSceneName, false, false));
        }
        mOnSceneLoad.Invoke(pSceneNames);
        MenuManager.Instance.HideLoad();
    }

    IEnumerator LoadSceneWithDelay(string pSceneName, bool pShowLoadingScreen = true, bool pRaiseEvents = true)
    {
        if(mCurrentLoadedScenes.Contains(pSceneName))
        {
            yield break;
        }
        else
        {
            if(pShowLoadingScreen)
            {
                MenuManager.Instance.ShowLoad();
            }
            yield return new WaitForSeconds(mDelayTime);

            Application.backgroundLoadingPriority = ThreadPriority.Low;

            AsyncOperation aSync = SceneManager.LoadSceneAsync(pSceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => aSync.isDone);

            Application.backgroundLoadingPriority = ThreadPriority.Normal;

            yield return new WaitForSeconds(mDelayTime);

            mCurrentLoadedScenes.Add(pSceneName);
            if (pRaiseEvents)
            {
                mOnSceneLoad.Invoke(mCurrentLoadedScenes);
            }

            if (pShowLoadingScreen)
            {
                MenuManager.Instance.HideLoad();
            }
        }
    }
}
