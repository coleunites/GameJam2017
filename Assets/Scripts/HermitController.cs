using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class HermitController : MonoBehaviour
{

    public GameObject mSpriteObject;

    public float mAdvanceDuration;
    public float mRetreatDuration;

    public Vector2 mAdvancePosition;
    Vector2 mIntialPos;

    float mTimeElpased;

    public bool mWillSurvive = false;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        mWillSurvive = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        mWillSurvive = false;
    }
}
