using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UiManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject button_4; 

    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;
    public Button fourthButton; 

    private Text firstButtonText;
    private Text secondButtonText;
    private Text thirdButtonText;
    private Text fourthButtonText;

    public Text header; 

    public bool gameOver = false;
    private bool fourthButtonIsActive = false; 

    // Start is called before the first frame update
    void Start()
    {
        firstButtonText = firstButton.GetComponentInChildren<Text>();
        secondButtonText = secondButton.GetComponentInChildren<Text>();
        thirdButtonText = thirdButton.GetComponentInChildren<Text>();
        fourthButtonText = fourthButton.GetComponentInChildren<Text>();

        StartMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            PauseMenu();
        }

        if(gameOver == true)
        {
            GameOverMenu();
        }

        if(fourthButtonIsActive == true)
        {
            button_4.SetActive(true);
        }
        else if(fourthButtonIsActive == false)
        {
            button_4.SetActive(false);
        }
    }

    void StartGame()
    {
        if (gameOver == true)
        {
            gameOver = false;
        }

        menu.SetActive(false);
        Debug.Log("start");
    }

    void QuitGame()
    {
        Debug.Log("quit");
    }

    void SettingsMenu()
    {
        Debug.Log("settings");
    }

    void PauseMenu()
    {
        header.text = "Pause";

        menu.SetActive(true);

        firstButton.onClick.AddListener(ResumeGame);
        firstButtonText.text = "Resume";

        secondButton.onClick.AddListener(StartMenu);
        secondButtonText.text = "Start Menu";

        thirdButton.onClick.AddListener(SettingsMenu);
        thirdButtonText.text = "Settings";

        button_4.SetActive(true);
        fourthButton.onClick.AddListener(QuitGame);
        fourthButtonText.text = "Quit";
    }

    void ResumeGame()
    {
        menu.SetActive(false);
    }

    void StartMenu()
    {
        header.text = "Game Title";

        firstButton.onClick.AddListener(StartGame);
        firstButtonText.text = "Start Game";

        secondButton.onClick.AddListener(SettingsMenu);
        secondButtonText.text = "Settings";

        thirdButton.onClick.AddListener(QuitGame);
        thirdButtonText.text = "Quit";

        button_4.SetActive(false);
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

        button_4.SetActive(false);
    }
}
