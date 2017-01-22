using UnityEngine;
using System.Collections;

public class ConstantRotation2D : MonoBehaviour
{
    public float mDegreesPerSec;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(transform.forward * mDegreesPerSec * Time.deltaTime);
    }
}
