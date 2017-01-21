using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingManager : MonoBehaviour {

    //RingManager holds and controls the queue for each individual ring entering the game

    //items for game flow
    #region Gameplay
    private Queue<RingController> ringQueue;
    public HermitController hermit;
    public float scaleAboveLastMultiplier;
    public float speedMultiplier = 1.0f; 
    public float startingRingSpeed;
    public float startingRotationRange;
    public float rotationMultiplier = 1.0f;
    public float destroyRingSize;
    public float rotationDegrees = 5.625f;

    private float currentScaleSpeed;
    private float currentRotationRange;
    private float scaleOfLast = 5.0f;
    #endregion

    //items for controls and ring manipulation
    #region ControlVariables
    public int selectedRing;
    public float selectedUpscale;
    public int upperSelectionLimit = 5;
    #endregion

    void Awake ()
    {
        ringQueue = new Queue<RingController>();
        currentScaleSpeed = startingRingSpeed;
        currentRotationRange = startingRotationRange;
        selectedRing = -1;
	}

    void Update()
    {
        //update scale speed and rotation range
        currentScaleSpeed *= speedMultiplier;
        currentRotationRange *= rotationMultiplier;
        if (scaleOfLast > 5.0f)
        {
            float percentage = currentScaleSpeed;
            percentage *= Time.deltaTime * 0.1f; // multiply by 0.01f to convert of percentage to a nume of 0 to 1;
            percentage = 1.0f - percentage; // get the sacle factor
            scaleOfLast = scaleOfLast * percentage;
        }

        //controls for selecting rings
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && selectedRing < upperSelectionLimit)
        {
            int old = selectedRing;
            SelectRing(++selectedRing, old);
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && selectedRing > 0)
        {
            int old = selectedRing;
            SelectRing(--selectedRing, old);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            ShiftRing(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            ShiftRing(-1);
        }

        //update each ring
        foreach (RingController ring in ringQueue)
        {
            ring.Scale(currentScaleSpeed);
        }

        //check the smallest ring if it is close to us and check if we will survive it
        if (ringQueue.Count > 0 && ringQueue.Peek().gameObject.transform.localScale.x <= destroyRingSize)
        {
            //tell hermit controller to check if we survive this ring
            if (hermit.CheckIfSurvies() || true) //TEMPORARY '|| true'
            {
                //set this ring to kill itself and remove it from the queue
                //update the selected ring
                if (selectedRing >= 0)
                {
                    selectedRing -= 1;
                }
                else
                {
                    SelectRing(selectedRing);
                }
                ringQueue.Dequeue().DestroyRing(currentScaleSpeed);
            }
            else
            {
                //lose the game
                //hermit dies
                ringQueue.Dequeue().DestroyRing(currentScaleSpeed);
            }
        }

    }

    private void SelectRing(int newRing, int oldring = -1)
    {
        int count = 0;
        foreach (RingController ring in ringQueue)
        {
            if (count == newRing)
            {
                //ring.gameObject.transform.localScale = new Vector3(ring.gameObject.transform.localScale.x + selectedUpscale, 1.0f, ring.gameObject.transform.localScale.z + selectedUpscale);
                ring.SetSpriteColor(Color.white);
            }
            if (count == oldring)
            {
                //ring.gameObject.transform.localScale = new Vector3(ring.gameObject.transform.localScale.x - selectedUpscale, 1.0f, ring.gameObject.transform.localScale.z - selectedUpscale);
                ring.SetSpriteColor(Color.black);
            }
            count++;
        }

    }

    private void ShiftRing(int direction) //left 0, right 1
    {
        int count = 0;
        foreach (RingController ring in ringQueue)
        {
            if (count == selectedRing)
            {
                ring.Rotate(rotationDegrees * direction);
                break;
            }
            count++;
        }

    }

    public void AddRingToQueue(RingController newRing )
    {
        //set it's size relitive to the last
        float newScale = scaleOfLast * scaleAboveLastMultiplier;
        scaleOfLast = newScale;
        newRing.gameObject.transform.localScale = new Vector3(newScale, newScale, 1.0f);
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
