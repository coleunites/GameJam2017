using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public GameObject obj_GameOver;
    public GameObject obj_InGame;
    public GameObject obj_TitleScreen;
    private ImageSwap[] img_DirInput;
    private Text txt_Score;

    enum CurrentState
    {
        TitleScreen,
        InGame,
        LoseGame
    }

    private CurrentState currentState;

	// Use this for initialization
	void Start ()
    {
        img_DirInput = new ImageSwap[4];
        img_DirInput[0] = obj_InGame.transform.FindChild("Img_Up").GetComponent<ImageSwap>();
        img_DirInput[1] = obj_InGame.transform.FindChild("Img_Down").GetComponent<ImageSwap>();
        img_DirInput[2] = obj_InGame.transform.FindChild("Img_Left").GetComponent<ImageSwap>();
        img_DirInput[3] = obj_InGame.transform.FindChild("Img_Right").GetComponent<ImageSwap>();
        txt_Score = obj_InGame.transform.FindChild("Txt_Score").GetComponent<Text>();

        currentState = CurrentState.TitleScreen;
        EnableNew(CurrentState.InGame); //should start on title screen but we don't have that yet. 
	}
	
	// Update is called once per frame
	void Update ()
    {
        //update in the various states
        switch (currentState)
        {
            case CurrentState.TitleScreen:
                break;
            case CurrentState.InGame:
                //check for button input (kinda ugly but whatevs) ¯\_(ツ)_/¯ 
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    img_DirInput[0].SwitchToSprite(1);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    img_DirInput[1].SwitchToSprite(1);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    img_DirInput[2].SwitchToSprite(1);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    img_DirInput[3].SwitchToSprite(1);
                }

                if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
                {
                    img_DirInput[0].SwitchToSprite(0);
                }
                if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
                {
                    img_DirInput[1].SwitchToSprite(0);
                }
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
                {
                    img_DirInput[2].SwitchToSprite(0);
                }
                if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
                {
                    img_DirInput[3].SwitchToSprite(0);
                }
                break;
            case CurrentState.LoseGame:
                break;
            default:
                break;
        }
    }

    public void GameOver()
    {

        currentState = CurrentState.LoseGame;
        //set the score of the game over screen. 

    }

    public void UpdateScore(int newScore)
    {
        txt_Score.text = "Score: " + newScore;
    }

    private void EnableNew(CurrentState newState)
    {
        //disable old
        GetRelevent(currentState).SetActive(false);
        //enable new
        GetRelevent(newState).SetActive(true);
        currentState = newState;
    }

    private GameObject GetRelevent(CurrentState thisState)
    {
        switch (thisState)
        {
            case CurrentState.TitleScreen:
                return obj_TitleScreen;
            case CurrentState.InGame:
                return obj_InGame;
            case CurrentState.LoseGame:
                return obj_GameOver;
            default:
                Debug.Log("[UiManager.cs] Default state triggered");
                return null;
        }
    }
    

}
