using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;

    public static SceneLoader Instance { get; private set; }

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

    public void LoadScenes(string mainSceneName, string additiveSceneName)
    {
        StartCoroutine(LoadMainAndAdditiveSceneCoroutine(mainSceneName, additiveSceneName));
    }

    private IEnumerator LoadSingleSceneCoroutine(string sceneName)
    {
        loadingCanvas.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!op.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        loadingCanvas.SetActive(false);
    }

    private IEnumerator LoadMainAndAdditiveSceneCoroutine(string targetScene, string additiveScene)
    {
        loadingCanvas.SetActive(true);

        AsyncOperation mainLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
        mainLoad.allowSceneActivation = false;

        AsyncOperation additiveLoad = SceneManager.LoadSceneAsync(additiveScene, LoadSceneMode.Additive);
        additiveLoad.allowSceneActivation = false;

        while (mainLoad.progress < 0.9f || additiveLoad.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        mainLoad.allowSceneActivation = true;
        additiveLoad.allowSceneActivation = true;

        while (!mainLoad.isDone || !additiveLoad.isDone)
            yield return null;

        yield return new WaitForSeconds(1f);

        loadingCanvas.SetActive(false);
    }
}
