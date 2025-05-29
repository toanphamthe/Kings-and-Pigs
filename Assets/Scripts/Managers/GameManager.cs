using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadSceneAsync("PlayerUI", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                Time.timeScale = 1f; // Resume the game
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Player player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            if (player != null)
            {
                Vector2 fakeAttackerPosition = player.transform.position + Vector3.left;
                player.PlayerTakeDamage.TakeDamage(1, fakeAttackerPosition);
            }
            else
            {
                Debug.Log("Player not found...");
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerHealthPresenter playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
            if (playerHealthPresenter != null)
            {
                playerHealthPresenter.Damage(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerHealthPresenter playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
            if (playerHealthPresenter != null)
            {
                playerHealthPresenter.Heal(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerHealthPresenter playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
            if (playerHealthPresenter != null)
            {
                playerHealthPresenter.ResetHealth();
            }
        }
    }
}
