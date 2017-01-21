﻿using UnityEngine;
using System.Collections;

public class RingController : MonoBehaviour
{
    float mPerpetualMovement = 0.0f;
    public SpriteRenderer mSprite;
    Color mAlpha;

    float mDistanceToStartFade = 0.0f;
    float mDistanceToEndFade = 0.0f;
    float mAlphaMax = 0.0f;

    void Start()
    {
        mAlpha = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(transform.forward * mPerpetualMovement * Time.deltaTime);
        if(mSprite.bounds.extents.x < mDistanceToStartFade && mSprite.bounds.extents.x > mDistanceToEndFade)
        {

 
        }
    }

    public void SetSpriteColor(Color color)
    {
        mSprite.color = color; 
    }

    //Negitive degrees is left, postive degrees is right
    public void SetPerpetualMotion(float speedInDeg)
    {
        mPerpetualMovement = speedInDeg;
    }

    //Negitive degrees is left, postive degrees is right
    public void Rotate(float degrees)
    {
        transform.Rotate(transform.forward * degrees);
    }

    //postive percetage will scale down, negtive percetnage will scale up
    public void Scale(float percentage)
    {
        percentage *= Time.deltaTime * 0.1f; // multiply by 0.01f to convert of percentage to a nume of 0 to 1;
        percentage = 1.0f - percentage; // get the sacle factor
        transform.localScale = transform.localScale * percentage;
    }

    public void DestroyRing(float timeScale)
    {
        Destroy(this.gameObject);
    }

    public void SetFadeDistances(float distToStartFade, float distToEndFade)
    {
        mDistanceToEndFade = distToEndFade;
        mDistanceToStartFade = distToStartFade;
        mAlphaMax = distToStartFade - distToStartFade;
    }
}
