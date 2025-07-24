using UnityEngine;

public class PigHideBoxLookingOut : MonoBehaviour, IPigHideBoxLookingOut
{
    [SerializeField] private float _lookingOutCooldown;
    [SerializeField] private float _lookingOutTimer;
    public float LookingOutCooldown => _lookingOutCooldown;
    public float LookingOutTimer => _lookingOutTimer;

    public void UpdateLookingOutTimer()
    {
        _lookingOutTimer += Time.deltaTime;
    }

    public void ResetLookingOutTimer()
    {
        _lookingOutTimer = 0f;
    }
}
