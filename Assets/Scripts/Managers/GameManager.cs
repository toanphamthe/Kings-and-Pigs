using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    private float _gameSpeed;

    [SerializeField]
    private enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync("PlayerUI", LoadSceneMode.Additive);
    }

    void Update()
    {
        //AdjustGameSpeed();
    }

    private void AdjustGameSpeed()
    {
        // Adjust the game speed based on the _gameSpeed variable
        Time.timeScale = _gameSpeed;
    }
}
