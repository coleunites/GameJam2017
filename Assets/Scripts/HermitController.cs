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
public class HermitController : MonoBehaviour
{

    public GameObject mSpriteObject;
    public Sprite mDeathSprite;


    public float mAdvanceDuration;
    public float mRetreatDuration;

    public Vector3 mAdvancePosition;
    Vector3 mIntialPos;

    public HermitState mCurState = HermitState.idle;

    public float mNormalSpeed = 0.5f;
    public float mAdvancingSpeed = 2.5f;

    float mTimeElpased;

    bool mWillSurvive = true;

    float mSpeedFactor = 1.0f;

    Animator mAnim;

    void Start()
    {
        mTimeElpased = 0.0f;
        mIntialPos = mSpriteObject.transform.position;
        mAnim = mSpriteObject.GetComponent<Animator>();
    }

    void Update()
    {
        mTimeElpased += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            mTimeElpased = 0.0f;
            mCurState = HermitState.advancing;
        }

        switch(mCurState)
        {
            case HermitState.idle:
                mAnim.speed = mNormalSpeed;
                break;
            case HermitState.advancing:
                mAnim.speed = mAdvancingSpeed;

                mSpriteObject.transform.position = 
                    Vector3.Lerp(mIntialPos, mAdvancePosition, mTimeElpased /mAdvanceDuration);
                if(mAdvanceDuration < mTimeElpased)
                {
                    mCurState = HermitState.retreating;
                    mTimeElpased = 0.0f;
                }
                break;
            case HermitState.retreating:
                mSpriteObject.transform.position =
                    Vector3.Lerp(mAdvancePosition, mIntialPos, mTimeElpased /mRetreatDuration);
                if (mRetreatDuration < mTimeElpased)
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
