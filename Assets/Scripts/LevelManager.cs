﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public RingManager mRingManager;
    public List<Section> mSections;
    public List<Pattern> mPatterns;

    public int mMinNumRings = 12;

    public float mSectionCoolDown = 2.0f;

    [Range(0.0f, 1.0f)]
    public float mEndOfSectionSpeedReduction = 0.15f;

    public float mDistToStartFadeRingsIn = Mathf.Infinity;
    public float mDistToEndFadeRingsIn = Mathf.Infinity;

    int mCurSection = 0;
    int mPatternsLeftInSection = 0;

    float mCoolDownTimeRemaining = 0.0f;
    public float mFirstSongFadeIn = 1.0f;
	// Use this for initialization
	void Start ()
    {
        mPatternsLeftInSection = mSections[mCurSection].mNumOfPatterns;
        mRingManager.SetScaleFactor(mSections[mCurSection].mSectionScaleFactor);
        AddPattern();
        MusicManager.GetInstance().FadeInSong(mSections[mCurSection].mSectionSong, mFirstSongFadeIn);     
    }
	
	// Update is called once per frame
	void Update ()
    {
        int ringNum = mRingManager.GetQueueCount();

        if(ringNum == 0 && mPatternsLeftInSection == 0)
        {
            mCurSection = (mCurSection + 1) % mSections.Count;
            mCoolDownTimeRemaining = mSectionCoolDown;
            mPatternsLeftInSection = mSections[mCurSection].mNumOfPatterns;
            mRingManager.SetScaleFactor(mSections[mCurSection].mSectionScaleFactor);
            mRingManager.EndSection(mEndOfSectionSpeedReduction);
            MusicManager.GetInstance().FadeInSong(mSections[mCurSection].mSectionSong, mSectionCoolDown);
        }
        else if (ringNum < mMinNumRings && mCoolDownTimeRemaining < 0.0f)
        {
            AddPattern();
        }
        else
        {
            //Place Color Lerp Here
        }
        mCoolDownTimeRemaining -= Time.deltaTime;
	}

    void AddPattern()
    {
        if (mPatternsLeftInSection > 0)
        {
            int patternNum = Random.Range(mSections[mCurSection].mStartOfSection, mSections[mCurSection].mEndOfSection + 1);
            Pattern curPattern = mPatterns[patternNum];
            for (int i = 0; i < curPattern.mRings.Count; ++i)
            {
                GameObject newRing = Instantiate(curPattern.mRings[i]);
                RingController newRingController = newRing.GetComponent<RingController>();
                mSections[mCurSection].mSectionColor.a = 0.0f;
                newRingController.SetSpriteColor(mSections[mCurSection].mSectionColor);
                mSections[mCurSection].mSectionColor.a = 1.0f;
                newRingController.SetFadeDistances(mDistToStartFadeRingsIn, mDistToEndFadeRingsIn);
                mRingManager.AddRingToQueue(newRingController, mSections[mCurSection].mSectionRotationDeg);
            }
            --mPatternsLeftInSection;
        }
    }
}

[System.Serializable]
public class Section
{
    public int mStartOfSection = 0;
    public int mEndOfSection = 0;
    public int mNumOfPatterns = 0;
    public Color mSectionColor;
    public float mSectionScaleFactor = 1.0f;
    public Songs mSectionSong = Songs.slow;
    public float mSectionRotationDeg = 0.0f;
}

[System.Serializable]
public class Pattern
{
    public List<GameObject> mRings;
}