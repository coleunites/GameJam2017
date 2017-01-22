using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Detector : MonoBehaviour
{
    public SpriteRenderer mSpriteRenderer;
    public Sprite mNoRingsDetectedSprite;
    public Sprite mRingsDetectedSprite;
    public Animator mAnim;

    List<uint> mRingsInDetecion;
    uint mClosetRing = 1;
    bool mIsColliding = false;


    public void SetClosestRing(uint id)
    {
        mClosetRing = id;
        if( id == mRingsInDetecion.Find(x => x == id))
        {
            mIsColliding = true;
        }
        else
        {
            mIsColliding = false;
        }
    }

    public void RemoveId(uint id)
    {
        mRingsInDetecion.Remove(id);
    }

    // Use this for initialization
    void Start ()
    {
        mRingsInDetecion = new List<uint>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        mAnim.SetBool("RingsNearBy", mIsColliding);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        uint newId = other.GetComponentInParent<RingController>().GetId();
        if(newId == mClosetRing)
        {
            mIsColliding = true;
        }

        mRingsInDetecion.Add(newId);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        uint newId = other.GetComponentInParent<RingController>().GetId();
        if (newId == mClosetRing)
        {
            mIsColliding = false;
        }

        mRingsInDetecion.Remove(newId);
    }
}
