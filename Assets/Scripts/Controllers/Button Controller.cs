using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private Text _scoreText;
    private Diamond _diamond;

    private void Start()
    {
        if (_uiCanvas != null)
        {
            _uiCanvas.transform.localScale = Vector3.zero;

            _uiCanvas.SetActive(true);

            _uiCanvas.transform.DOScale(Vector3.one, 0.5f);
        }

        _diamond = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Diamond>();
        if (_diamond != null)
        {
            _diamond.OnDiamondChanged += UpdateScore;
            UpdateScore();
        }
    }

    public void OnMainMenuButtonClick()
    {
        GameManager.Instance.LoadMainMenu();
        SoundManager.Instance.PlaySFX("Button");
    }

    public void OnNextLevelButtonClick()
    {
        GameManager.Instance.LoadNextLevel();
        SoundManager.Instance.PlaySFX("Button");
    }

    public void OnPauseButtonClick()
    {
        GameManager.Instance.SetGameState(GameState.Paused);
        SoundManager.Instance.PlaySFX("Button");
    }

    public void OnResumeButtonClick()
    {
        GameManager.Instance.SetGameState(GameState.Playing);
        SoundManager.Instance.PlaySFX("Button");
    }

    public void OnReplayButtonClick()
    {
        GameManager.Instance.LoadCurrentLevel();
        SoundManager.Instance.PlaySFX("Button");
    }

    private void UpdateScore()
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"Score: {_diamond.CurrentDiamond}";
        }
    }
}
