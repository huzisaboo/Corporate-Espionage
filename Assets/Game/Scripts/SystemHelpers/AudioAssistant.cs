using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioAssistant : MonoBehaviour
{
    AudioSource mAudioSource;
    string mSourceID;
    [SerializeField] AudioGroup mAudioGroup = AudioGroup.Sfx;
    void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
        if(mAudioSource == null || mAudioGroup == AudioGroup.None)
        {
            Destroy(this);
            return;
        }
        mSourceID = System.Guid.NewGuid().ToString();
        AudioManager.Instance.AddAmbientSource(mSourceID, mAudioGroup, mAudioSource);
    }

    void OnDestroy()
    {
        if(AudioManager.IsValidSingleton())
        {
            AudioManager.Instance.RemoveAmbientSource(mSourceID);
        }
    }

    public void PlaySound(string pSoundId, bool pLoop = false, float pDelay = 0)
    {
        AudioManager.Instance.PlaySound(pSoundId, pLoop, mSourceID, pDelay);
    }
}
