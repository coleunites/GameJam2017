using UnityEngine;
using System.Collections;

public class RingController : MonoBehaviour
{
    float mPerpetualMovement = 0.0f;
    public SpriteRenderer mSprite;
    Color mAlpha;

    public GameObject mCollider;

    public float mDestroyedScale = 0.05f;
    public float mDestroyedDuration = 0.5f;

    bool mDestroyed = false;

    float mDistanceToStartFade = Mathf.Infinity;
    float mDistanceToEndFade = Mathf.Infinity;
    float mAlphaMax = 1.0f;
    float mTimeElpased = 0.0f;
    Vector3 mScaleBeforeDeath;

    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(transform.forward * mPerpetualMovement * Time.deltaTime);
        if(mDestroyed)
        {
            mTimeElpased += Time.deltaTime;
            float t = mTimeElpased / mDestroyedDuration;
            mAlpha.a = Mathf.Lerp(1.0f, 0.0f, t);
            mSprite.color = mAlpha;
            transform.localScale = Vector3.Lerp(mScaleBeforeDeath, new Vector3(mDestroyedScale, mDestroyedScale), t);
            if(mTimeElpased >= mDestroyedDuration)
            {
                Destroy(this.gameObject);
            }
        }
        else if(mSprite.bounds.extents.x < mDistanceToStartFade)
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
    }

    public void SetSpriteColor(Color color)
    {
        mSprite.color = color; 
    }

    public void SetSpriteColorAlphaIndependent(Color color)
    {
        Color newColor = new Color(color.r, color.g, color.b, mSprite.color.a);
        mSprite.color = newColor;
    }

    public Color GetSpriteColor()
    {
        return mSprite.color;
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
        if(mCollider == null)
        {
            mCollider = gameObject.GetComponentInChildren<Collider2D>().gameObject;
        }
        mCollider.SetActive(false);
        mDestroyed = true;
        mScaleBeforeDeath = transform.localScale;
        //Destroy(this.gameObject);
    }

    public void SetFadeDistances(float distToStartFade, float distToEndFade)
    {
        mDistanceToEndFade = distToEndFade;
        mDistanceToStartFade = distToStartFade;
        mAlphaMax = distToStartFade - distToEndFade;
    }
}
