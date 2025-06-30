using System;
using UnityEngine;

public class Diamond : MonoBehaviour, IDiamond
{
    [SerializeField] private int _currentDiamond;
    public int CurrentDiamond
    {
        get => _currentDiamond;
        set => _currentDiamond = value;
    }

    public event Action OnDiamondChanged;

    // Initializes the diamond count to zero or a specified value
    public void DecreaseDiamond(int amount)
    {
        _currentDiamond -= amount;
        UpdateDiamond();
    }

    // Increases the diamond count by a specified amount
    public void IncreaseDiamond(int amount)
    {
        _currentDiamond += amount;
        UpdateDiamond();
    }

    // Restores the diamond count to zero (or a specific value if needed)
    public void RestoreDiamond()
    {
        _currentDiamond = 0; // Assuming restoring means resetting to zero
    }

    // This method is called to update the diamond count and trigger the OnDiamondChanged event
    public void UpdateDiamond()
    {
        OnDiamondChanged?.Invoke();
    }
}
