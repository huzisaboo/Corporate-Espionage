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

    public enum Result
    {
        Won,
        Lost
    }

    public SceneReference mGameScene;
    public SceneReference mUIScene;
    [HideInInspector] public State mState = State.NonGame;
    public MenuClassifier mEndMenuClassifier;
    EndMenu mEndMenu;
    void Awake()
    {
        LeanTween.init(1000);
    }

    void Start()
    {
        MultiSceneManager.Instance.mOnSceneLoad.AddListener(OnSceneLoad);
        MultiSceneManager.Instance.mOnSceneUnload.AddListener(OnSceneUnload);
        if(string.IsNullOrEmpty(mUIScene)) //remove after testing
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

    public void EndGame()
    {
        mEndMenu = MenuManager.Instance.GetMenu<EndMenu>(mEndMenuClassifier);
    }
}
