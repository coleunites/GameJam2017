using UnityEngine;
using System.Collections;

public class ConstantRotation2D : MonoBehaviour
{
    public float mDegreesPerSec;
    public float mMaxSpeed = 15.0f;

    float mSpeed = 1.0f;

    public void AffectSpeed(float multiplier)
    {
        float deltaSpeed = mSpeed * Time.deltaTime * multiplier;
        mSpeed = Mathf.Clamp(mSpeed + deltaSpeed, 0.0f, mMaxSpeed);
    }

	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(transform.forward * mDegreesPerSec * mSpeed * Time.deltaTime);
    }
}
