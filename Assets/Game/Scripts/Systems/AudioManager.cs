///-------------------------------------------------------------------------------------------------
// File: AudioManager.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary: Singleton to manage Ambient and Non-Ambient audio
///-------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
[Serializable]
public enum AudioGroup
{
    None,
    Music,
    Ambient,
    Sfx,
    UI
}

/// <summary>
/// Structure that holds the audio clip and the audio group of the clip
/// </summary>
[Serializable]
public struct AudioData
{
    public AudioClip mAudioClip;
    public AudioGroup mAudioGroup;
}

/// <summary>
/// Structure that holds the way of fading in and out of the audio sources
/// </summary>
public struct AudioFadeData
{
    public AudioSource mAudioSource;
    public string mAudioID;
    public float mDv;
    public bool mFadingIn;
    public float mDelay;
}
/// <summary>
/// The object of this class is the audio source that is currently playing
/// </summary>
public class AudioCurrentlyPlaying
{
    public AudioSource mAudioSource;
    public float mNonPlayingTimer;

    public AudioCurrentlyPlaying(AudioSource pAudioSrc)
    {
        mAudioSource = pAudioSrc;
        mNonPlayingTimer = 0;
    }
}

/// <summary>
/// The structure to hold the mixer data for easier instantiation
/// </summary>
[Serializable]
public struct AudioMixerData
{
    public AudioGroup mAudioGroup;
    public AudioMixerGroup mMixerGroup;
    public string mExposedVolVariableName;
    public int mNoOfAudioSources;
}

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer mMixer;
    public string mMasterVolumeName = "MasterVol";
    public List<AudioMixerData> mAudioMixerData;
    public List<AudioData> mAudioData;

    readonly Dictionary<string, int> mAudioDataDic = new Dictionary<string, int>();
    readonly Dictionary<AudioGroup, List<AudioSource>> mAudioSources = new Dictionary<AudioGroup, List<AudioSource>>();
    readonly Dictionary<string, AudioCurrentlyPlaying> mAudioSrcPlaying = new Dictionary<string, AudioCurrentlyPlaying>();
    readonly Dictionary<string, AudioSource> mTDSources = new Dictionary<string, AudioSource>();

    readonly List<AudioFadeData> mFadingData = new List<AudioFadeData>();

    float mTimerUpdatePlaying = 0;


    /// <summary>
    /// Instantiates the audio sources and assigns the above dictionaries for optimization
    /// </summary>
    void Start()
    {
        if (mAudioData != null && mAudioData.Count > 0)
        {
            for (int aIx = 0; aIx < mAudioData.Count; aIx++)
            {
                if (mAudioData[aIx].mAudioClip == null)
                {
                    continue;
                }
                string aAudioClipName = mAudioData[aIx].mAudioClip.name;
                mAudioDataDic[aAudioClipName] = aIx;
            }
        }
        if (mAudioMixerData != null && mAudioMixerData.Count > 0)
        {
            for (int aIx = 0; aIx < mAudioMixerData.Count; aIx++)
            {
                if(mAudioMixerData[aIx].mAudioGroup == AudioGroup.None)
                {
                    continue;
                }
                if(!mAudioSources.ContainsKey(mAudioMixerData[aIx].mAudioGroup))
                {
                    List<AudioSource> aSrcVec = new List<AudioSource>(mAudioMixerData[aIx].mNoOfAudioSources);
                    for (int aJx = 0; aJx < mAudioMixerData[aIx].mNoOfAudioSources; aJx++)
                    {
                        var aGObj = new GameObject(string.Format("AudioSrc_Grp{0}_Ix_{1}", aIx, aJx));
                        aGObj.transform.SetParent(transform);
                        AudioSource aSource = aGObj.AddComponent<AudioSource>();
                        aSource.outputAudioMixerGroup = mAudioMixerData[aIx].mMixerGroup;
                        aSource.playOnAwake = false;
                        aSrcVec.Add(aSource);
                    }
                    mAudioSources.Add(mAudioMixerData[aIx].mAudioGroup, aSrcVec);
                }
            }


        }
    }

    public void SetAudioVolumeFromSave()
    {

    }



    /// <summary>
    /// Checks all playing audiosources timers and stops them when their timer runs out or fades them out
    /// </summary>
    void Update()
    {

        mTimerUpdatePlaying -= Time.deltaTime;
        if (mTimerUpdatePlaying <= 0 && !AudioListener.pause)
        {
            mTimerUpdatePlaying = 0.1f;
            List<string> aKeysToRemove = new List<string>();
            foreach (var aKeyValPair in mAudioSrcPlaying)
            {
                var aPlayingData = aKeyValPair.Value;
                if (aPlayingData.mAudioSource.time < (aPlayingData.mAudioSource.clip.length - 0.1f) || aPlayingData.mAudioSource.isPlaying)
                {
                    aPlayingData.mNonPlayingTimer = 0;
                    continue;
                }
                else
                {
                    aPlayingData.mNonPlayingTimer += Time.deltaTime;
                    if (aPlayingData.mNonPlayingTimer < 2.0f)
                    {
                        continue;
                    }
                }

                aKeysToRemove.Add(aKeyValPair.Key);
            }

            if (aKeysToRemove.Count > 0)
            {
                foreach (var aKey in aKeysToRemove)
                {
                    mAudioSrcPlaying.Remove(aKey);

                }
                aKeysToRemove.Clear();
            }
        }


        if (mFadingData.Count > 0)
        {
            for (int aI = 0; aI < mFadingData.Count; aI++)
            {
                var aFadeInfo = mFadingData[aI];
                aFadeInfo.mDelay -= Time.deltaTime;
                if (aFadeInfo.mDelay > 0)
                {
                    mFadingData[aI] = aFadeInfo;
                    continue;
                }

                aFadeInfo.mAudioSource.volume += aFadeInfo.mDv * Time.deltaTime;

                if (aFadeInfo.mFadingIn)
                {
                    if (aFadeInfo.mAudioSource.volume >= 0.999f)
                    {
                        aFadeInfo.mAudioSource.volume = 1.0f;
                        mFadingData.RemoveAt(aI);
                    }
                }
                else
                {
                    if (aFadeInfo.mAudioSource.volume <= 0.001f)
                    {
                        aFadeInfo.mAudioSource.volume = 0f;
                        aFadeInfo.mAudioSource.Stop();
                        mFadingData.RemoveAt(aI);

                        if (mAudioSrcPlaying.ContainsKey(aFadeInfo.mAudioID))
                        {
                            mAudioSrcPlaying.Remove(aFadeInfo.mAudioID);
                        }
                    }

                }
            }
        }


    }

    /// <summary>
    /// Function to get an empty audio source of the given audio group
    /// </summary>
    /// <param name="pGroup">audio group</param>
    /// <returns>audio source</returns>
    AudioSource GetEmptySource(AudioGroup pGroup)
    {
        if (!mAudioSources.ContainsKey(pGroup))
        {
            return null;
        }

        var aSources = mAudioSources[pGroup];

        for (int aI = 0; aI < aSources.Count; aI++)
        {
            if (!aSources[aI].isPlaying)
            {
                return aSources[aI];
            }
        }

        return null;
    }

    /// <summary>
    /// Function called to play audio sfx
    /// </summary>
    /// <param name="pSoundID">the name of the audio clip</param>
    /// <param name="pDelay">the delay before playing sfx</param>
	public void PlaySound(string pSoundID, bool pLoop = false, string pSourceID = null, float pDelay = 0)
    {
        if (mAudioDataDic.ContainsKey(pSoundID))
        {
            var aAudioData = mAudioData[mAudioDataDic[pSoundID]];
            AudioSource aAudioSrc;

            if (string.IsNullOrEmpty(pSourceID) || !mTDSources.ContainsKey(pSourceID))
            {
                aAudioSrc = GetEmptySource(aAudioData.mAudioGroup);
            }
            else
            {
                aAudioSrc = mTDSources[pSourceID];
            }

            if (aAudioSrc == null)
            {
                return;
            }

            aAudioSrc.playOnAwake = false;
            aAudioSrc.clip = aAudioData.mAudioClip;
            aAudioSrc.loop = pLoop;
            if (pDelay <= 0.001f)
            {
                aAudioSrc.Play();
            }
            else
            {
                aAudioSrc.PlayDelayed(pDelay);
            }

            mAudioSrcPlaying[pSoundID] = new AudioCurrentlyPlaying(aAudioSrc);
        }
    }

    /// <summary>
    /// Stop the mentioned sound id to be playing
    /// </summary>
    /// <param name="pSoundID">name of sound clip</param>
	public void StopSound(string pSoundID)
    {
        if (!mAudioSrcPlaying.ContainsKey(pSoundID))
        {
            return;
        }

        mAudioSrcPlaying[pSoundID].mAudioSource.Stop();
        mAudioSrcPlaying.Remove(pSoundID);
    }

    /// <summary>
    /// Fade the said sound in the given timer and delay
    /// </summary>
    /// <param name="pSoundID">name of sound</param>
    /// <param name="pFadeTimer">timer of fading</param>
    /// <param name="pFadeOut">if true then fade out</param>
    /// <param name="pDelay">delay in fading</param>
	public void FadeSound(string pSoundID, float pFadeTimer, bool pFadeOut, float pDelay = 0)
    {
        if (!mAudioSrcPlaying.ContainsKey(pSoundID))
        {
            return;
        }


        int aFoundIx = -1;
        for (int aIx = 0; aIx < mFadingData.Count; aIx++)
        {
            if (mFadingData[aIx].mAudioID == pSoundID)
            {
                aFoundIx = aIx;
                break;
            }
        }

        if (aFoundIx > -1)
        {
            mFadingData.RemoveAt(aFoundIx);

        }

        var aAudioSrc = mAudioSrcPlaying[pSoundID].mAudioSource;
        AudioFadeData aFadeData = new AudioFadeData();

        aFadeData.mAudioSource = aAudioSrc;

        if (pFadeOut)
        {
            aFadeData.mDv = -aAudioSrc.volume / pFadeTimer;
        }
        else
        {
            if (aAudioSrc.volume > 0.99f)
            {
                aAudioSrc.volume = 0;
                aFadeData.mDv = 1 / pFadeTimer;
            }
            else
            {
                aFadeData.mDv = (1 - aAudioSrc.volume) / pFadeTimer;
            }
        }

        aFadeData.mFadingIn = !pFadeOut;
        aFadeData.mDelay = pDelay;
        aFadeData.mAudioID = pSoundID;

        mFadingData.Add(aFadeData);
    }


    /// <summary>
    /// Function to set the volume of the mixer
    /// </summary>
    /// <param name="pMixerGroupName">mixer group</param>
    /// <param name="pVolume">volume</param>
	public void SetMixerGroupVolume(AudioGroup pMixerGroupName, float pVolume)
    {
        if(pMixerGroupName == AudioGroup.None)
        {
            mMixer.SetFloat(mMasterVolumeName, pVolume);
            return;
        }
        for (int aI = 0; aI < mAudioMixerData.Count; aI++)
        {
            if (mAudioMixerData[aI].mAudioGroup == pMixerGroupName)
            {
                mMixer.SetFloat(mAudioMixerData[aI].mExposedVolVariableName, pVolume);
                break;
            }
        }
    }

    /// <summary>
    /// Gets the mixer volume that is set currently
    /// </summary>
    /// <param name="pMixerGroupName">Name of the mixer group</param>
    /// <returns></returns>
    public float GetMixerGroupVolume(AudioGroup pMixerGroupName)
    {
        float aValue = 0.0f;
        if(pMixerGroupName == AudioGroup.None)
        {
            mMixer.GetFloat(mMasterVolumeName, out aValue);
        }
        else
        {
            for (int aI = 0; aI < mAudioMixerData.Count; aI++)
            {
                if (mAudioMixerData[aI].mAudioGroup == pMixerGroupName)
                {
                    mMixer.GetFloat(mAudioMixerData[aI].mExposedVolVariableName, out aValue);
                    break;
                }
            }
        }
        return aValue;
    }

    /// <summary>
    /// Check whether the sound is playing or not
    /// </summary>
    /// <param name="pSoundID">name of sound</param>
    /// <returns>true is sound playing</returns>
	public bool IsSoundPlaying(string pSoundID)
    {
        if (!mAudioSrcPlaying.ContainsKey(pSoundID))
        {
            return false;
        }

        return true;

    }

    /// <summary>
    /// Function to add a 3D Audio Source to Audio Manager
    /// </summary>
    /// <param name="pSourceId">Unique Source ID GUID</param>
    /// <param name="pMixerGroup">Mixer Group</param>
    /// <param name="pAudioSource">3D Audio Source</param>
    public void AddAmbientSource(string pSourceId, AudioGroup pMixerGroup, AudioSource pAudioSource)
    {
        if (pAudioSource == null)
        {
            return;
        }
        if (!mAudioSources.ContainsKey(pMixerGroup))
        {
            return;
        }
        pAudioSource.outputAudioMixerGroup = mAudioSources[pMixerGroup][0].outputAudioMixerGroup;
        mTDSources.Add(pSourceId, pAudioSource);
    }

    /// <summary>
    /// Remove the source ID once the usage is over
    /// </summary>
    /// <param name="pSourceId">Source ID</param>
    public void RemoveAmbientSource(string pSourceId)
    {
        if (string.IsNullOrEmpty(pSourceId) || !mTDSources.ContainsKey(pSourceId))
        {
            return;
        }
        mTDSources.Remove(pSourceId);
    }


}
