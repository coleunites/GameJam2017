using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class RingManager : MonoBehaviour {

    //RingManager holds and controls the queue for each individual ring entering the game

    //items for game flow
    #region Sound
    public float soundCoolDown = 0.5f;
    AudioSource audioSource;
    float soundTimer = 0.0f;
    #endregion

    public ConstantRotation2D whirlPool;
    public Animator mermaidAnim;

    #region Gameplay
    private Queue<RingController> ringQueue;
    public HermitController hermit;
    public float scaleAboveLastMultiplier;
    public float speedMultiplier = 1.0f; 
    public float startingRingSpeed;
    public float rotationMultiplier = 1.0f;
    public float destroyRingSize;
    public float rotationDegrees = 5.625f;
    public UiManager uiManager;
    public float firstRing = 4.0f;

    public Color selectedColor = Color.white;
    public Detector detector;
    public float maxScaleSpeed = Mathf.Infinity;

	public float currentScaleSpeed;
    private Color oldColor;
    private Color newColor;
    private float currentRotationRange;
    private float scaleOfLast;
    private float scaleSpeedTracker = 0.0f;
    private float prevScalePeriod = 0.0f;
    private int ringCounter;
    #endregion

    //items for controls and ring manipulation
    #region ControlVariables
    private int selectedRing;
    public int upperSelectionLimit = 5;
    public float timeBetweenShifts = 0.15f;
    private float shiftTimer;
    #endregion

    //Items for fine control of the wave
    public float amplitude = 0.8f;
    public float frequency = 2.0f;


    void Awake ()
    {
        ringQueue = new Queue<RingController>();
        currentScaleSpeed = startingRingSpeed;
        selectedRing = -1;
        ringCounter = 0;
        scaleOfLast = firstRing;

        audioSource = GetComponent<AudioSource>();
        soundTimer = soundCoolDown;
	}

    void Update()
    {
        soundTimer += Time.deltaTime;

        //update scale speed and rotation range
        prevScalePeriod = currentScaleSpeed;
        float deltaSpeed = currentScaleSpeed * Time.deltaTime * speedMultiplier;
		currentScaleSpeed = Mathf.Clamp(currentScaleSpeed + deltaSpeed, -maxScaleSpeed, maxScaleSpeed);

        hermit.MultiplySpeedFactor(speedMultiplier + 1);
        whirlPool.AffectSpeed(speedMultiplier);

        scaleSpeedTracker += currentScaleSpeed - prevScalePeriod;

        currentRotationRange *= rotationMultiplier;

        //for keeping scaleOfLast Aligned to the in game rings
        if (scaleOfLast > firstRing)
        {
            /*float percentage = currentScaleSpeed;
            percentage *= Time.deltaTime * 0.1f; // multiply by 0.01f to convert of percentage to a nume of 0 to 1;
            percentage = 1.0f - percentage; // get the sacle factor
            scaleOfLast = scaleOfLast * percentage;*/

            scaleOfLast -= (currentScaleSpeed * Time.deltaTime * 0.5f);

        }

        //controls for selecting rings
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (selectedRing < upperSelectionLimit)
            {
                int old = selectedRing;
                SelectRing(++selectedRing, old);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (selectedRing > 0)
            {
                int old = selectedRing;
                SelectRing(--selectedRing, old);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            shiftTimer += Time.deltaTime;
            if (shiftTimer >= timeBetweenShifts)
            {
                shiftTimer = 0.0f;
                ShiftRing(1);
                if(soundTimer >= soundCoolDown)
                {
                    audioSource.Play();
                    soundTimer = 0.0f;
                }
            }

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            shiftTimer += Time.deltaTime;
            if (shiftTimer >= timeBetweenShifts)
            {
                shiftTimer = 0.0f;
                ShiftRing(-1);
                if (soundTimer >= soundCoolDown)
                {
                    audioSource.Play();
                    soundTimer = 0.0f;
                }
            }
        }
        else
        {
            shiftTimer = timeBetweenShifts;
        }

        //update each ring
        if (uiManager.CheckPlaying())
        {
            foreach (RingController ring in ringQueue)
            {
                ring.Scale(currentScaleSpeed);
            }
        }
        //check the smallest ring if it is close to us and check if we will survive it
        if (ringQueue.Count > 0 && ringQueue.Peek().gameObject.transform.localScale.x <= destroyRingSize)
        {
            //tell hermit controller to check if we survive this ring
            if (hermit.CheckIfSurvies())
            {
                detector.RemoveId(ringQueue.Peek().GetId());
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
                uiManager.UpdateScore(++ringCounter);
                if(ringQueue.Count>0)
                {
                    detector.SetClosestRing(ringQueue.Peek().GetId());
                }
            }
            else
            {
                //lose the game
                //hermit dies
                uiManager.GameOver();
                ringQueue.Dequeue().DestroyRing(currentScaleSpeed);
                mermaidAnim.SetTrigger("Death");
            }

        }

        if (ringQueue.Count > 0)
        {
            detector.SetClosestRing(ringQueue.Peek().GetId());
            if(selectedRing == -1)
            {
                selectedRing = 0;
                SelectRing(0);
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
                newColor = ring.GetSpriteColor();
                ring.SetSpriteColorAlphaIndependent(selectedColor);
            }
            if (count == oldring)
            {
                //ring.gameObject.transform.localScale = new Vector3(ring.gameObject.transform.localScale.x - selectedUpscale, 1.0f, ring.gameObject.transform.localScale.z - selectedUpscale);
                ring.SetSpriteColorAlphaIndependent(oldColor);
            }
            count++;
        }

        oldColor = newColor;
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

    public void AddRingToQueue(RingController newRing, float perpetualRotationRange = 0.0f)
    {
        //set it's size relitive to the last
        float newScale = scaleOfLast * scaleAboveLastMultiplier * 0.6f;
        scaleOfLast = newScale;
        newRing.SetSineWave(newScale, destroyRingSize, frequency, amplitude);
        newRing.Scale(currentScaleSpeed);
        //set it's perpetual rotation
        newRing.SetPerpetualMotion(Random.Range(perpetualRotationRange, -perpetualRotationRange));
        
        //add ring to the queue
        ringQueue.Enqueue(newRing);
    }

    public int GetQueueCount()
    {
        return ringQueue.Count;
    }
    
    public void SetScaleFactor(float scaleFactor)
    {
        scaleAboveLastMultiplier = scaleFactor;
    }

    public void EndSection(float percentage)
    {
        currentScaleSpeed -= (scaleSpeedTracker * percentage);
        scaleSpeedTracker = 0.0f;
    }
}
