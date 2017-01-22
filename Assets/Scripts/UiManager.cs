using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public GameObject obj_GameOver;
    private ImageSwap img_GameOver;
    public GameObject obj_InGame;
    public GameObject obj_TitleScreen;
    private ImageSwap img_SpaceToPlay;
    private ImageSwap[] img_DirInput;
    private Text txt_Score;

    public bool playingGame;

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
        txt_Score = this.transform.FindChild("Txt_Score").GetComponent<Text>();
        img_GameOver = obj_GameOver.transform.FindChild("Img_GameOver").GetComponent<ImageSwap>();
        img_SpaceToPlay = obj_TitleScreen.transform.FindChild("Img_SpaceToPlay").GetComponent<ImageSwap>();


        currentState = CurrentState.TitleScreen;
        EnableNew(CurrentState.TitleScreen);
        img_SpaceToPlay.Play(true, 0.80f);
        PauseGame();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //update in the various states
        switch (currentState)
        {
            case CurrentState.TitleScreen:
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    EnableNew(CurrentState.InGame);
                    PlayGame();
                }
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
                //Space to restart
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    //restart the game
                    EnableNew(CurrentState.TitleScreen);
                }
                break;
            default:
                break;
        }
    }

    public void GameOver()
    {
        EnableNew(CurrentState.LoseGame);
        img_GameOver.Play();
        PauseGame();
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

    private void PauseGame()
    {
        playingGame = false;
        Time.timeScale = 0;
    }

    private void PlayGame()
    {
        playingGame = true;
        Time.timeScale = 1;
    }

    public bool CheckPlaying()
    {
        return playingGame;
    }

}
