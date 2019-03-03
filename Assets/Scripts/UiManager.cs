using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UiManager : MonoBehaviour
{
    public GameObject menu;
    //public GameObject button_3; 

    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;

    private Text firstButtonText;
    private Text secondButtonText;
    private Text thirdButtonText;

    public Text header; 

    public bool gameOver = false;
    private bool isPlaying = false;

    public LevelManager levelManager; 

    // Start is called before the first frame update
    void Start()
    {
        firstButtonText = firstButton.GetComponentInChildren<Text>();
        secondButtonText = secondButton.GetComponentInChildren<Text>();
        thirdButtonText = thirdButton.GetComponentInChildren<Text>();

        StartMenu();

        thirdButtonText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            GameSystem.instance.gameState = GameState.Pause;
        }

        if(GameSystem.instance.gameState == GameState.StartMenu)
        {
            StartMenu();
        }

        if (GameSystem.instance.gameState == GameState.Playing && isPlaying == true)
        {
            ResumeGame();
        }

        if (GameSystem.instance.gameState == GameState.Pause)
        {
            PauseMenu();
        }

        if(GameSystem.instance.gameState == GameState.GameOver)
        {
            GameOverMenu();
        }
    }

    void StartGame()
    {
        if (gameOver == true)
        {
            gameOver = false;
        }

        menu.SetActive(false);

        GameSystem.instance.gameState = GameState.Playing;

        if(isPlaying == false)
        {
            isPlaying = true;
        }
    }

    void QuitGame()
    {
        levelManager.QuitGame();
    }

    void PauseMenu()
    {
        menu.SetActive(true);

        header.text = "Pause";

        firstButton.onClick.AddListener(ResumeGame);
        firstButtonText.text = "Resume";

        secondButton.onClick.AddListener(StartMenu);
        secondButtonText.text = "Start Menu";


        thirdButton.onClick.AddListener(QuitGame);
        thirdButtonText.text = "Quit";
    }

    void ResumeGame()
    {
        menu.SetActive(false);
    }

    void StartMenu()
    {
        if(!menu.activeInHierarchy)
        {
            menu.SetActive(true);
        }

        if(gameOver == true)
        {
            gameOver = false; 
        }

        header.text = "Game Title";

        firstButton.onClick.AddListener(StartGame);
        firstButtonText.text = "Start Game";

        secondButton.onClick.AddListener(QuitGame);
        secondButtonText.text = "Quit";

        thirdButtonText.text = "";
    }

    void GameOverMenu()
    {
        menu.SetActive(true);

        header.text = "Game Over";

        firstButton.onClick.AddListener(StartGame);
        firstButtonText.text = "Retry";

        secondButton.onClick.AddListener(StartMenu);
        secondButtonText.text = "Start Menu";


        thirdButton.onClick.AddListener(QuitGame);
        thirdButtonText.text = "Quit";

    }
}
