using UnityEngine;
using System.Collections;

public enum Songs
{
    slow,
    meduim,
    intense
}

public class MusicManager : MonoBehaviour
{
    public enum MusicState
    {
        firstFadeIn,
        crossFadeIn1To2,
        crossFadeIn2To1,
        allFadeOut,
        playing,
        none
    }

    static MusicManager instance;

    static public MusicManager GetInstance()
    {
        return instance;
    }

    public AudioClip mSlowSong;
    public AudioClip mMeduimSong;
    public AudioClip mIntenseSong;

    public AudioSource [] mAudioSource;
    int mCurAudioSource = 0;
    float mTimeElpased;
    float mCurFadeDur;

    public MusicState mCurState = MusicState.none;

    public void FadeInSong(Songs song, float fadeInDuration)
    {
        if(mCurState == MusicState.none)
        {
            mCurState = MusicState.firstFadeIn;
            SetSong(song, mAudioSource[0]);
            mAudioSource[0].Play();
        }
        else
        {
            if(mAudioSource[0].isPlaying)
            {
                mCurState = MusicState.crossFadeIn1To2;
                SetSong(song, mAudioSource[1]);
                mAudioSource[1].Play();
            }
            else
            {
                mCurState = MusicState.crossFadeIn2To1;
                SetSong(song, mAudioSource[0]);
                mAudioSource[0].Play();
            }
        }
        mCurFadeDur = fadeInDuration;
        mTimeElpased = 0.0f;
    }

    public void FadeOutAllMusic(float fadeOutDuration)
    {

        mCurFadeDur = fadeOutDuration;
    }

    void FadeIn(AudioSource audioSource)
    {
        audioSource.volume = mTimeElpased / mCurFadeDur;
    }

    void FadeOut(AudioSource audioSource)
    {
        audioSource.volume = (mCurFadeDur - mTimeElpased) / mCurFadeDur;
    }

    void SetSong(Songs song, AudioSource audioSource)
    {
        switch (song)
        {
            case Songs.slow:
                audioSource.clip = mSlowSong;
                break;
            case Songs.meduim:
                audioSource.clip = mMeduimSong;
                break;
            case Songs.intense:
                audioSource.clip = mIntenseSong;
                break;
        }
    }

    // Use this for initialization
    void Awake ()
    {
        instance = this;
	}
	
    void OnDestroy()
    {
        instance = null;
    }

	// Update is called once per frame
	void Update ()
    {
        mTimeElpased += Time.unscaledDeltaTime;
        switch(mCurState)
        {
            case MusicState.allFadeOut:
                FadeOut(mAudioSource[0]);
                FadeOut(mAudioSource[1]);
                break;
            case MusicState.firstFadeIn:
                FadeIn(mAudioSource[0]);
                break;
            case MusicState.crossFadeIn1To2:
                FadeIn(mAudioSource[1]);
                FadeOut(mAudioSource[0]);
                break;
            case MusicState.crossFadeIn2To1:
                FadeIn(mAudioSource[0]);
                FadeOut(mAudioSource[1]);
                break;
            case MusicState.playing:
            case MusicState.none:
                break;
        }
        if(mTimeElpased >= mCurFadeDur && mCurState != MusicState.playing)
        {
            if(mCurState == MusicState.allFadeOut)
            {
                mAudioSource[0].Stop();
                mAudioSource[1].Stop();
            }
            else if (mCurState != MusicState.firstFadeIn)
            {
                mAudioSource[mCurAudioSource].Stop();
                mCurAudioSource = (mCurAudioSource + 1) % mAudioSource.Length;
            }
            mCurState = MusicState.playing;
        }
	}
}
