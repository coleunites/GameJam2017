using UnityEngine;
using System.Collections;

public class RingController : MonoBehaviour {

    public float mPerpetualMovement = 0.0f;

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Rotate(-15.0f * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(15.0f * Time.deltaTime);
        }
        //Scale(1.0f);

        transform.forward = Quaternion.AngleAxis(mPerpetualMovement * Time.deltaTime, Vector3.up) * transform.forward;
    }

    //Negitive degrees is left, postive degrees is right
    public void SetPerpetualMotion(float speedInDeg)
    {
        mPerpetualMovement = speedInDeg;
    }

    //Negitive degrees is left, postive degrees is right
    public void Rotate(float degrees)
    {
        transform.forward = Quaternion.AngleAxis(degrees * Time.deltaTime, Vector3.up) * transform.forward;
    }

    //postive percetage will scale down, negtive percetnage will scale up
    public void Scale(float percentage)
    {
        percentage *= Time.deltaTime * 0.1f; // multiply by 0.01f to convert of percentage to a nume of 0 to 1;
        percentage = 1.0f - percentage; // get the sacle factor
        transform.localScale = transform.localScale * percentage;
    }
}
