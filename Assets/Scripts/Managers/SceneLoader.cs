using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;

    public static SceneLoader Instance { get; private set; }

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration;

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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSingleSceneCoroutine(sceneName));
    }

    public void LoadAdditiveScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    public void LoadScenes(string mainSceneName, string additiveSceneName, GameState stateAfterLoad)
    {
        StartCoroutine(LoadMainAndAdditiveSceneCoroutine(mainSceneName, additiveSceneName, stateAfterLoad));
    }

    private IEnumerator FadeOut()
    {
        fadeCanvasGroup.alpha = 0;
        fadeCanvasGroup.gameObject.SetActive(true);
        yield return fadeCanvasGroup.DOFade(1, fadeDuration).WaitForCompletion();
    }

    private IEnumerator FadeIn()
    {
        yield return fadeCanvasGroup.DOFade(0, fadeDuration).WaitForCompletion();
        fadeCanvasGroup.gameObject.SetActive(false);
    }

    private IEnumerator LoadSingleSceneCoroutine(string sceneName)
    {
        loadingCanvas.SetActive(true);

        yield return StartCoroutine(FadeOut());

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!op.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(FadeIn());

        loadingCanvas.SetActive(false);
    }

    private IEnumerator LoadMainAndAdditiveSceneCoroutine(string targetScene, string additiveScene, GameState stateAfterLoad)
    {
        loadingCanvas.SetActive(true);

        yield return StartCoroutine(FadeOut());

        AsyncOperation mainLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
        mainLoad.allowSceneActivation = false;

        while (mainLoad.progress < 0.9f)
        {
            yield return null;
        }

        mainLoad.allowSceneActivation = true;

        while (!mainLoad.isDone)
            yield return null;

        AsyncOperation additiveLoad = SceneManager.LoadSceneAsync(additiveScene, LoadSceneMode.Additive);

        while (!additiveLoad.isDone)
            yield return null;

        yield return new WaitForSeconds(0.2f);

        GameManager.Instance.SetGameState(stateAfterLoad);

        yield return StartCoroutine(FadeIn());

        loadingCanvas.SetActive(false);
    }
}
