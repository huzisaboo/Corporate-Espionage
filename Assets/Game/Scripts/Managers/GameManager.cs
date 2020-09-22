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
    [System.Serializable]
    public enum State
    {
        NonGame,
        Game
    }

    public SceneReference mGameScene;
    public SceneReference mUIScene;
    public MenuClassifier mGameEndMenu;
    [HideInInspector] public State mState = State.NonGame;
    EndMenu mEndGameMenu;
    void Awake()
    {
        LeanTween.init(1000);
    }

    void Start()
    {
        MultiSceneManager.Instance.mOnSceneLoad.AddListener(OnSceneLoad);
        MultiSceneManager.Instance.mOnSceneUnload.AddListener(OnSceneUnload);
        if (string.IsNullOrEmpty(mUIScene)) //remove after testing
        {
            mState = State.Game;
            return;
        }
        MultiSceneManager.Instance.LoadScene(mUIScene);
        AudioManager.Instance.PlaySound("JazzyVibes", true);
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

    public void StartGame()
    {
        mState = State.Game;
        MultiSceneManager.Instance.LoadScene(mGameScene);
    }

    public void EndGame(EndMenu.Result pResult, GameEndReason pReason, string pRemainingTime,
        float pCompletedPercentage)
    {
        if (mEndGameMenu == null)
        {
            mEndGameMenu = MenuManager.Instance.GetMenu<EndMenu>(mGameEndMenu);
        }
        mEndGameMenu.DisplayEndReason(pReason);
        mEndGameMenu.DisplayResult(pResult);
        mEndGameMenu.MissionsCompleted(pCompletedPercentage);
        mEndGameMenu.DisplayRemainingTime(pRemainingTime);
        MultiSceneManager.Instance.UnloadScene(mGameScene);
        mState = State.NonGame;
        MenuManager.Instance.ShowMenu(mGameEndMenu);
        MultiSceneManager.Instance.UnloadScene(mGameScene);
        mState = State.NonGame;
    }
}
