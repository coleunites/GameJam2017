using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class HermitController : MonoBehaviour
{
    public bool mWillSurvive = false;

    public bool CheckIfSurvies()
    {
        return mWillSurvive;
    }

    public void SurvivedRing()
    {
        mWillSurvive = false;
        // play animation
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
