using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnExitButton()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void OnClose(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void OnOpen(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
