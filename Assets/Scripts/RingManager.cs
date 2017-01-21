using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingManager : MonoBehaviour {

    //RingManager holds and controls the queue for each individual ring entering the game
    private Queue<RingController> ringQueue;
    public float scaleAboveLastMultiplier;
    public float speedMultiplier;
    public float startingRingSpeed;
    public float destroyRingSize;

    private float currentScaleSpeed;
    private float scaleOfLast = 1.0f;

	void Start ()
    {
        currentScaleSpeed = startingRingSpeed;
	}
	
	void Update ()
    {
        currentScaleSpeed *= speedMultiplier;
        foreach (RingController ring in ringQueue)
        {
            ring.Scale(currentScaleSpeed);
            if (ring.gameObject.transform.localScale.x <= destroyRingSize)
            {
                //tell hermit controller to check if we survive this ring
                //if so delete this ring, if not game over.
            }
        }
	}

    public void AddRingToQueue(RingController newRing )
    {
        //set it's size relitive to the last
        float newScale = scaleOfLast * scaleAboveLastMultiplier;
        scaleOfLast = newScale;
        newRing.gameObject.transform.localScale = new Vector3(newScale, 1.0f, newScale);  
        ringQueue.Enqueue(newRing);
    }

    

}
