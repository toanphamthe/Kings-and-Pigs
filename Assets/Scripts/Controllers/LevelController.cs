using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelButtons;

    private void Start()
    {
        ShowUnlockedLevels();
    }

    private void ShowUnlockedLevels()
    {
        int unlockedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.UnlockedLevel, 1);

        for (int i = 0; i < levelButtons.Count; i++)
        {
            int levelNumber = i + 1;
            bool isUnlocked = levelNumber <= unlockedLevel;
            
            levelButtons[i].gameObject.SetActive(isUnlocked);

            if (isUnlocked)
            {
                levelButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();

                levelButtons[i].GetComponentInChildren<Text>().text = string.Format("{0}", levelNumber);

                levelButtons[i].GetComponent<Button>().onClick.AddListener(() => OnClickLevel(levelNumber));
            }
        }
    }

    public void OnClickLevel(int level)
    {
        SceneLoader.Instance.LoadScenes($"Lv_{level}", "PlayerUI", GameState.Playing);
    }
}
