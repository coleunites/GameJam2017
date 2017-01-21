using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSwap : MonoBehaviour {
    public Sprite[] spriteList;
    private int currentSprite;
	// Use this for initialization
	void Start ()
    {
        currentSprite = 0;
        if (spriteList.Length > 0) 
            this.GetComponent<Image>().sprite = spriteList[0];
	}

    //switches sprite to next in array
    public void NextSprite()
    {
        currentSprite++;
        if (currentSprite >= spriteList.Length || currentSprite < 0)
            currentSprite = 0;

        this.GetComponent<Image>().sprite = spriteList[currentSprite];
    }

    //switches sprite to one at specific index. Will switch to index 0 if specified index is out of range.  
    public void SwitchToSprite(int index)
    {
        currentSprite = index - 1;
        NextSprite();
        
    }
}
