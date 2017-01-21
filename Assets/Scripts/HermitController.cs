using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class HermitController : MonoBehaviour
{
    

    public GameObject mSpriteObject;

    public float mAdvanceDuration;
    public float mRetreatDuration;

    public Vector3 mAdvancePosition;
    Vector3 mIntialPos;

    float mTimeElpased;

    bool mWillSurvive = false;
    bool mPlayAdavanceAnim = false;
    bool mIsRetreating = false;

    float mSpeedFactor = 1.0f;

    void Start()
    {
        mTimeElpased = 0.0f;
        mIntialPos = transform.position;
    }

    void Update()
    {
        //if(mPlayAdavanceAnim)
        //{
        //    mTimeElpased += Time.deltaTime;
        //    if (!mIsRetreating)
        //    {
        //        if (mTimeElpased >= mAdvanceDuration && !mIsRetreating)
        //        {
        //            mIsRetreating = true;
        //            pos
        //            mTimeElpased = 0.0f;
        //        }
        //        else
        //        {
        //
        //        }
        //    }
        //    else
        //    {
        //        if (mTimeElpased > mRetreatDuration)
        //        {
        //            mPlayAdavanceAnim = false;
        //            mIsRetreating = false;
        //            mTimeElpased = 0.0f;
        //        }
        //        else
        //        {
        //
        //        }
        //    }
        //}
    }

    public bool CheckIfSurvies()
    {

        mWillSurvive = false;
        // play animation
        mPlayAdavanceAnim = true;
        return mWillSurvive;
    }

    void OnTriggerEnter(Collider other)
    {
        mWillSurvive = true;
    }

    void OnTriggerExit(Collider other)
    {
        mWillSurvive = false;
    }
}
