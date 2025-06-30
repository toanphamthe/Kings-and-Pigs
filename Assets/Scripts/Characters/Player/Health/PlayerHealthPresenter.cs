using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPresenter : MonoBehaviour
{
    [SerializeField] private IPlayerHealth _playerHealth;
    [SerializeField] private Image[] _hearts;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<IPlayerHealth>();
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged += OnHealthChanged;
        }
        UpdateView();
    }

    // This method is called when the object is destroyed to unsubscribe from the health change event
    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged -= OnHealthChanged;
        }
    }

    // This method is called to apply damage to the player
    public void Damage(int amount)
    {
        _playerHealth?.DecreaseHealth(amount);
    }

    // This method is called to heal the player
    public void Heal(int amount)
    {
        _playerHealth?.IncreaseHealth(amount);
    }

    // This method is called to reset the player's health to the maximum value
    public void ResetHealth()
    {
        _playerHealth?.RestoreHealth();
    }

    // This method updates the health display based on the player's current health
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

    // This method is called when the player's health changes to update the view
    public void OnHealthChanged()
    {
        UpdateView();
    }
}
