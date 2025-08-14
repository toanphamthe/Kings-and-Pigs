using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private bool _isMusicEnabled = true;
    [SerializeField] private bool _isSfxEnabled = true;

    [SerializeField] private Slider _volumeSlider;
    [Range(0f, 100f)]
    [SerializeField] private float _volume;

    public void ToggleMusic()
    {
        _isMusicEnabled = !_isMusicEnabled;
        SoundManager.Instance.IsMusicEnabled(_isMusicEnabled);
    }

    public void ToggleSfx()
    {
        _isSfxEnabled = !_isSfxEnabled;
        SoundManager.Instance.IsSFXEnabled(_isSfxEnabled);
    }

    private void Update()
    {
        VolumeSettings();
    }

    private void VolumeSettings()
    {
        _volume = _volumeSlider.value * 100f;
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Volume, _volumeSlider.value);
    }
}
