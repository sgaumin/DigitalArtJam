using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Singleton
    public static LevelManager instance;

    // Initialize Singleton
    private void Awake()
    {
        instance = this;
    }

    // Load Menu Scene
    public void LoadMenu()
    {
        SceneManager.LoadScene(Utilities.menuScene);
        Time.timeScale = 1f;
    }

    // Load Game Scene
    public void LoadGame()
    {
        AkSoundEngine.PostEvent("Play_Menu_Clic", gameObject);
        SceneManager.LoadScene(Utilities.gameScene);
    }

    // Reload the actual Scene
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}