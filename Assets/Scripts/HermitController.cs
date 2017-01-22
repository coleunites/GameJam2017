using UnityEngine;
using System.Collections;


public enum HermitState
{
    advancing,
    retreating,
    idle,
    death
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class HermitController : MonoBehaviour
{

    public GameObject mSpriteObject;
    public Sprite mDeathSprite;


    public float mAdvanceDuration;
    public float mRetreatDuration;
    public float mMinDuration = 0.05f;

    public Vector3 mAdvancePosition;
    Vector3 mIntialPos;

    public HermitState mCurState = HermitState.idle;

    public float mNormalSpeed = 0.5f;
    public float mAdvancingSpeed = 2.5f;

    float mTimeElpased;

    bool mWillSurvive = true;

    float mSpeedFactor = 1.0f;
    float mActualDuration = 0.0f;

    Animator mAnim;

    public void MultiplySpeedFactor(float amount)
    {
        mSpeedFactor *= amount;

        switch(mCurState)
        {
            case HermitState.advancing:
            case HermitState.retreating:
            case HermitState.death:
                break;

            case HermitState.idle:
                mActualDuration = Mathf.Clamp(mAdvanceDuration * (1 / mSpeedFactor), mMinDuration, Mathf.Infinity);
                break;
        }
    }

    void Start()
    {
        mTimeElpased = 0.0f;
        mIntialPos = mSpriteObject.transform.position;
        mAnim = mSpriteObject.GetComponent<Animator>();
    }

    void Update()
    {
        mTimeElpased += Time.deltaTime;

        switch(mCurState)
        {
            case HermitState.idle:
                mAnim.speed = mNormalSpeed;
                break;
            case HermitState.advancing:
                mAnim.speed = mAdvancingSpeed;
                mSpriteObject.transform.position = 
                    Vector3.Lerp(mIntialPos, mAdvancePosition, mTimeElpased / mActualDuration);
                if(mActualDuration < mTimeElpased)
                {
                    mCurState = HermitState.retreating;
                    mTimeElpased = 0.0f;
                    mActualDuration = Mathf.Clamp(mRetreatDuration * (1 / mSpeedFactor), mMinDuration, Mathf.Infinity);
                }
                break;
            case HermitState.retreating:
                mSpriteObject.transform.position =
                    Vector3.Lerp(mAdvancePosition, mIntialPos, mTimeElpased / mActualDuration);
                if (mActualDuration < mTimeElpased)
                {
                    mCurState = HermitState.idle;
                }
                break;
            case HermitState.death:
                mAnim.SetTrigger("Death");
                break;
        }
    }

    public bool CheckIfSurvies()
    {
        bool returnVal = mWillSurvive;

        if(mWillSurvive && mCurState != HermitState.death)
        {
            mCurState = HermitState.advancing;
            mTimeElpased = 0.0f;
        }
        else
        {
            mCurState = HermitState.death;
            GetComponent<AudioSource>().Play();
        }

		mWillSurvive = true;

        return returnVal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		mWillSurvive = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
		mWillSurvive = true;
    }
}
