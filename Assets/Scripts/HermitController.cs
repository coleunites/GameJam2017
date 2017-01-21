using UnityEngine;
using System.Collections;


enum HermitState
{
    advancing,
    retreating,
    idle,
}

[RequireComponent(typeof(Rigidbody2D))]
public class HermitController : MonoBehaviour
{

    public GameObject mSpriteObject;

    public float mAdvanceDuration;
    public float mRetreatDuration;

    public Vector2 mAdvancePosition;
    Vector2 mIntialPos;

    HermitState mCurState = HermitState.idle;

    float mTimeElpased;

    bool mWillSurvive = true;

    float mSpeedFactor = 1.0f;

    void Start()
    {
        mTimeElpased = 0.0f;
        mIntialPos = mSpriteObject.transform.position;
    }

    void Update()
    {
        mTimeElpased += Time.deltaTime;

        switch(mCurState)
        {
            case HermitState.idle:
                break;
            case HermitState.advancing:
                break;
            case HermitState.retreating:
                break;
        }
    }

    public bool CheckIfSurvies()
    {
        bool returnVal = mWillSurvive;
		mWillSurvive = true;

        // play animation
        mCurState = HermitState.advancing;
        mTimeElpased = 0.0f;

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
