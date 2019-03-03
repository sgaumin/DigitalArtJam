using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance { get; private set; }

    public GameState gameState = GameState.StartMenu;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InputManager.OnPause += PauseGame;
    }

    public void PauseGame()
    {
        if (gameState == GameState.Playing) {
            gameState = GameState.Pause;
            Time.timeScale = 0f;
        }

        if (gameState == GameState.Pause) {
            gameState = GameState.Playing;
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            {
            LevelManager.instance.LoadMenu();
            }
    }
}
