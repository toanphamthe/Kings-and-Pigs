using System;

public interface IDiamond
{
    int CurrentDiamond { get; set; }

    event Action OnDiamondChanged;

    void IncreaseDiamond(int amount);
    void DecreaseDiamond(int amount);
    void RestoreDiamond();
}