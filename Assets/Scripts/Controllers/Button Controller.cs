using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    private void Start()
    {
        if (_gameOverPanel != null)
        {
            _gameOverPanel.transform.localScale = Vector3.zero;

            _gameOverPanel.SetActive(true);

            _gameOverPanel.transform.DOScale(Vector3.one, 0.5f);
        }
    }

    public void OnMainMenuButtonClick()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void OnNextLevelButtonClick()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void OnPauseButtonClick()
    {
        GameManager.Instance.SetGameState(GameState.Paused);
    }

    public void OnResumeButtonClick()
    {
        GameManager.Instance.SetGameState(GameState.Playing);
    }

    public void OnReplayButtonClick()
    {
        GameManager.Instance.LoadCurrentLevel();
    }
}
