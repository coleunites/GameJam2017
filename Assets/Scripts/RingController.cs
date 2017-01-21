using UnityEngine;
using System.Collections;

public class RingController : MonoBehaviour {

    public float mTestScale = 0.0f;
    public float mPerpetualMovement = 0.0f;
    public SpriteRenderer mSprite;

    // Update is called once per frame
    void Update ()
    {
        transform.forward = Quaternion.AngleAxis(mPerpetualMovement * Time.deltaTime, Vector3.up) * transform.forward;
        Scale(mTestScale);
    }

    public void SetSpriteColor(Color color)
    {
        mSprite.color = color;
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

    public void DestroyRing(float timeScale)
    {
        Destroy(this.gameObject);
    }
}
