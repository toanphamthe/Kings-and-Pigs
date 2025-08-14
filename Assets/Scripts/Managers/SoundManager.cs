using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Background Music")]
    [SerializeField] private bool _isMusicEnabled = true;
    public List<AudioClipEntry> musicClipEntries;
    private Dictionary<string, AudioClip> _musicDict;
    [SerializeField] private AudioSource _backgroundMusicSource;

    [Header("SFX")]
    [SerializeField] private bool _isSFXEnabled = true;
    public List<AudioClipEntry> sfxClipEntries;
    private Dictionary<string, AudioClip> _sfxDict;
    [SerializeField] private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load Music
        _musicDict = new Dictionary<string, AudioClip>();
        foreach (var entry in musicClipEntries)
        {
            if (!_musicDict.ContainsKey(entry.name))
                _musicDict.Add(entry.name, entry.audioClip);
        }

        // Load SFX
        _sfxDict = new Dictionary<string, AudioClip>();
        foreach (var entry in sfxClipEntries)
        {
            if (!_sfxDict.ContainsKey(entry.name))
                _sfxDict.Add(entry.name, entry.audioClip);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Lv_"))
        {
            PlayBackgroundMusic("Playing");
        }
    }

    public void IsMusicEnabled(bool enable) => _isMusicEnabled = enable;
    public void IsSFXEnabled(bool enable) => _isSFXEnabled = enable;

    private void Update()
    {
        float volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.Volume, 0.5f);

        _backgroundMusicSource.volume = _isMusicEnabled ? volume : 0f;
        _sfxSource.volume = _isSFXEnabled ? volume : 0f;
    }

    // ===== MUSIC =====
    public void PlayBackgroundMusic(string name)
    {
        if (_musicDict.TryGetValue(name, out var clip))
        {
            _backgroundMusicSource.clip = clip;
            _backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music '{name}' not found!");
        }
    }

    // ===== SFX =====
    public void PlaySFX(string name)
    {
        if (_sfxDict.TryGetValue(name, out var clip))
        {
            _sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{name}' not found!");
        }
    }
}
