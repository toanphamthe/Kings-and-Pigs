using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [SerializeField] private float _gameSpeed;

    void Update()
    {
        PlayerTest();
        SceneTest();
    }

    private void SceneTest()
    {
        #region Reload Scene (Code: R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion
    }

    private void PlayerTest()
    {
        #region Player Health (Code: J K L)
        // Increase player health by 1
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerHealthPresenter playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
            if (playerHealthPresenter != null)
            {
                playerHealthPresenter.Damage(1);
            }
        }

        // Decrease player health by 1
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerHealthPresenter playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
            if (playerHealthPresenter != null)
            {
                playerHealthPresenter.Heal(1);
            }
        }

        // Reset player health to full
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerHealthPresenter playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
            if (playerHealthPresenter != null)
            {
                playerHealthPresenter.ResetHealth();
            }
        }
        #endregion

        #region Player Hit (Code: Q)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Player player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            if (player != null)
            {
                Vector2 fakeAttackerPosition = player.transform.position + Vector3.left;
                player.Hit.TakeDamage(1, fakeAttackerPosition);
            }
            else
            {
                Debug.Log("Player not found...");
            }
        }
        #endregion
    }
}
