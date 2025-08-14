using DG.Tweening;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
        gameObject.transform.DOScale(Vector3.zero, 0.3f)
     .SetEase(Ease.InBack)
     .OnComplete(() => gameObject.SetActive(false));
        
        SoundManager.Instance.PlaySFX("Button");


    }

    public void OnOpen(GameObject gameObject)
    {

        gameObject.transform.localScale = Vector3.zero;

        gameObject.SetActive(true);

        gameObject.transform.DOScale(Vector3.one, 0.5f);
        SoundManager.Instance.PlaySFX("Button");
    }
}
