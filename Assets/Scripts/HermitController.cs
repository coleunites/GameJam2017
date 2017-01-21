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

    float mSpeedFactor = 1.0f;

    void Start()
    {
        mTimeElpased = 0.0f;
        mIntialPos = transform.position;
    }

    void Update()
    {
        
    }

    public bool CheckIfSurvies()
    {
        bool returnVal = mWillSurvive;
        mWillSurvive = false;

        // play animation
        return returnVal;
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
