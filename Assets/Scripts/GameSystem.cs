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
}
