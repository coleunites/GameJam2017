using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSwap : MonoBehaviour {
    public Sprite[] spriteList;
    private int currentSprite;

    private float animateOverTime;
    private float timer;
    private bool playing;
    private bool loop;
	// Use this for initialization
	void Start ()
    {
        currentSprite = 0;
        playing = false;
        loop = false;
        if (spriteList.Length > 0) 
            this.GetComponent<Image>().sprite = spriteList[0];
	}

    void Update()
    {
        if (playing)
        {
            timer += Time.deltaTime;
            if (timer >= animateOverTime)
            {
                NextSprite();
            } 

        }

    }

    public void Play(bool doLoop = false, float playOver = 1.0f)
    {
        if(spriteList.Length > 0)
        animateOverTime = playOver / spriteList.Length;
        timer = 0.0f;
        loop = doLoop;
        playing = true;
    }

    //switches sprite to next in array
    public void NextSprite()
    {
        currentSprite++;
        if (currentSprite >= spriteList.Length || currentSprite < 0)
        {
            if (playing)
                if(!loop)
                    playing = false;
            else
                currentSprite = 0;
        }

        this.GetComponent<Image>().sprite = spriteList[currentSprite];
    }

    //switches sprite to one at specific index. Will switch to index 0 if specified index is out of range.  
    public void SwitchToSprite(int index)
    {
        currentSprite = index - 1;
        NextSprite();
        
    }
}
