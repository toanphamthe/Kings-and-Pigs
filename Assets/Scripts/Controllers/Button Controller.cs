using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
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
