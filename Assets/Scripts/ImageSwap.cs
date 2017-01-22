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
	void Awake ()
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
            timer += Time.unscaledDeltaTime;
            if (timer >= animateOverTime)
            {
                NextSprite();
                timer = 0.0f;
            } 

        }

    }

    public void Play(bool doLoop = false, float playOver = 0.6f)
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
        if (currentSprite < 0)
            currentSprite = 0;

        if (currentSprite >= spriteList.Length )
        {
            if (playing)
            {
                if (!loop)
                {
                    playing = false;
                    return;
                }
            }
            currentSprite = 0;
        }
        if (spriteList.Length > 0)
            this.GetComponent<Image>().sprite = spriteList[currentSprite];
    }

    //switches sprite to one at specific index. Will switch to index 0 if specified index is out of range.  
    public void SwitchToSprite(int index)
    {
        currentSprite = index - 1;
        NextSprite();
        
    }
}
