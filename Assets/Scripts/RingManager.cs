using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingManager : MonoBehaviour {

    //RingManager holds and controls the queue for each individual ring entering the game

    //items for game flow
    private Queue<RingController> ringQueue;
    public HermitController hermit;
    public float scaleAboveLastMultiplier;
    public float speedMultiplier = 1.0f; 
    public float startingRingSpeed;
    public float startingRotationRange;
    public float rotationMultiplier = 1.0f;
    public float destroyRingSize;

    private float currentScaleSpeed;
    private float currentRotationRange;
    private float scaleOfLast = 1.0f;

    //items for controls and ring manipulation
    int selectedRing;


	void Start ()
    {
        currentScaleSpeed = startingRingSpeed;
        currentRotationRange = startingRotationRange;
	}
	
	void Update ()
    {
        //update scale speed and rotation range
        currentScaleSpeed *= speedMultiplier;
        currentRotationRange *= rotationMultiplier;

        foreach (RingController ring in ringQueue)
        {
            ring.Scale(currentScaleSpeed);
            if (ring.gameObject.transform.localScale.x <= destroyRingSize)
            {
                //tell hermit controller to check if we survive this ring
                if (hermit.CheckIfSurvies())
                {
                    //set this ring to kill itself and remove it from the queue
                    ringQueue.Dequeue();
                }
                else
                {
                    //lose the game
                    //hermit dies
                }
            }
        }




	}

    public void AddRingToQueue(RingController newRing )
    {
        //set it's size relitive to the last
        float newScale = scaleOfLast * scaleAboveLastMultiplier;
        scaleOfLast = newScale;
        newRing.gameObject.transform.localScale = new Vector3(newScale, 1.0f, newScale);
        //set it's perpetual rotation
        newRing.SetPerpetualMotion(Random.Range(currentRotationRange, -currentRotationRange));
        
        //add ring to the queue
        ringQueue.Enqueue(newRing);
    }

    public int GetQueueCount()
    {
        return ringQueue.Count;
    }
    

}
