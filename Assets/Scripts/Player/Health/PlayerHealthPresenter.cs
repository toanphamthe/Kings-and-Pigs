using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPresenter : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Image[] _hearts;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerHealth>();
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged += OnHealthChanged;
        }
        UpdateView();
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= OnHealthChanged;
        }
    }

    public void Damage(int amount)
    {
        _playerHealth?.DecreaseHealth(amount);
    }

    public void Heal(int amount)
    {
        _playerHealth?.IncreaseHealth(amount);
    }

    public void ResetHealth()
    {
        _playerHealth?.RestoreHealth();
    }

    public void UpdateView()
    {
        if (_playerHealth == null)
            return;

        if (_hearts != null && _playerHealth.MaxHealth != 0)
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (i < _playerHealth.CurrentHealth)
                {
                    _hearts[i].gameObject.SetActive(true);
                }
                else
                {
                    _hearts[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnHealthChanged()
    {
        UpdateView();
    }
}
