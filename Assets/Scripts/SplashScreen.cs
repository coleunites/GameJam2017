using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public float mFadeInDuration= 3.5f;
    public float mWaitTime = 1.0f;
    public float mFadeOutDuration = 4.0f;
    public Image mLogo;

    public AudioSource mAudioSource;

    public string mSceneToLoad;

    Color mAlphaColor;
    bool mFadedin = false;
    bool mCanFadeOut = false;

    float mTimeElapsed = 0.0f;
	// Use this for initialization
	void Start ()
    {
        mAlphaColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        mLogo.color = mAlphaColor;
        mAlphaColor.a = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        mTimeElapsed += Time.deltaTime;
        if(mTimeElapsed >= mFadeInDuration && !mFadedin)
        {
            mTimeElapsed = 0.0f;
            mFadedin = true;
            mAudioSource.Play();
        }
        else if (!mFadedin)
        {
            mAlphaColor.a = mTimeElapsed / mFadeInDuration;
            mLogo.color = mAlphaColor;
        }
        else if(mTimeElapsed >= mWaitTime && !mCanFadeOut)
        {
            mTimeElapsed = 0.0f;
            mCanFadeOut = true;
        }
        else if(mTimeElapsed >= mFadeOutDuration && mCanFadeOut)
        {
            // Go to new scene
            if(mSceneToLoad != null && mSceneToLoad != "")
            {
                SceneManager.LoadScene(mSceneToLoad);
            }
        }
        else if(mCanFadeOut)
        {
            mAlphaColor.a = (mFadeOutDuration - mTimeElapsed) / mFadeOutDuration;
            mLogo.color = mAlphaColor;
        }
	}
}
