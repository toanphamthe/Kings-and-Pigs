using UnityEngine;

public interface IPigHideBoxLookingOut
{
    float LookingOutCooldown { get; }
    float LookingOutTimer { get; }
    void UpdateLookingOutTimer();
}
