using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }
    public delegate void OnGameStateChanged(GameState newState);
    public event OnGameStateChanged GameStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetGameState(GameState.MainMenu);
    }

    public void SaveDiamond(int diamond)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.Diamond, diamond);
    }    

    public void UnlockLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(sceneName.Replace("Lv_", ""));

        int unlockedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.UnlockedLevel, 1);
        int nextLevel = currentLevel + 1;

        if (nextLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.UnlockedLevel, nextLevel);
            PlayerPrefs.Save();
        }
    }

    public void SetGameState(GameState newState)
    {
        if (newState == CurrentState) return;

        CurrentState = newState;
        Debug.Log("Game State changed to: " + newState);

        GameStateChanged?.Invoke(newState);

        HandleGameState(newState);
    }

    private void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                if (!SceneManager.GetSceneByName("PlayerUI").isLoaded)
                {
                    SceneLoader.Instance.LoadAdditiveScene("PlayerUI");
                }
                SceneLoader.Instance.UnloadScene("GameOverUI");
                SceneLoader.Instance.UnloadScene("VictoryUI");
                SceneLoader.Instance.UnloadScene("PauseMenu");
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                SceneLoader.Instance.UnloadScene("PlayerUI");
                SceneLoader.Instance.LoadAdditiveScene("PauseMenu");
                break;
            case GameState.GameOver:
                SceneLoader.Instance.UnloadScene("PlayerUI");
                SceneLoader.Instance.LoadAdditiveScene("GameOverUI");
                break;
            case GameState.Victory:
                UnlockLevel();
                SceneLoader.Instance.UnloadScene("GameOverUI");
                SceneLoader.Instance.LoadAdditiveScene("VictoryUI");
                break;
            case GameState.MainMenu:
                SoundManager.Instance.PlayBackgroundMusic("MainMenu");
                Time.timeScale = 1f;
                break;
        }
    }

    public void LoadCurrentLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadScenes(currentSceneName, "PlayerUI", GameState.Playing);
    }

    public void LoadMainMenu()
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded) return;
        SceneLoader.Instance.LoadScene("MainMenu");
        SetGameState(GameState.MainMenu);
    }

    public void LoadNextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.StartsWith("Lv_"))
        {
            // Parse level number
            int currentLevel = int.Parse(currentSceneName.Substring(3));
            int nextLevel = currentLevel + 1;

            // Get PlayerPrefs for unlocked levels
            int unlockedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.UnlockedLevel, 1);

            // Check if the next level can be loaded
            string nextLevelName = "Lv_" + nextLevel;
            if (unlockedLevel >= nextLevel)
            {
                SceneLoader.Instance.LoadScene(nextLevelName);
                SetGameState(GameState.Playing);
            }
            else
            {
                LoadMainMenu();
            }
        }
    }
}
