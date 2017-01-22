using UnityEngine;
using System.Collections;

public class RingController : MonoBehaviour
{
    float mPerpetualMovement = 0.0f;
    public SpriteRenderer mSprite;
    Color mAlpha;

    float mDistanceToStartFade = Mathf.Infinity;
    float mDistanceToEndFade = Mathf.Infinity;
    float mAlphaMax = 1.0f;

    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(transform.forward * mPerpetualMovement * Time.deltaTime);
        if(mSprite.bounds.extents.x < mDistanceToStartFade)
        {
            mAlpha = mSprite.color;
            if(mSprite.bounds.extents.x > mDistanceToEndFade)
            {
                mAlpha.a =(mAlphaMax - (mSprite.bounds.extents.x - mDistanceToEndFade))/mAlphaMax;
            }
            else
            {
                mAlpha.a = 1.0f;
            }
            mSprite.color = mAlpha;
        }

        Debug.Log(mSprite.bounds.extents);
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
        mAlphaMax = distToStartFade - distToEndFade;
    }
}
