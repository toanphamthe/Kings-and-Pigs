using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Background Music")]
    [SerializeField] private bool _isMusicEnabled;
    public List<AudioClipEntry> audioClipEntries;
    private Dictionary<string, AudioClip> _audioClipsDict;
    [SerializeField] private AudioSource _backgroundMusicSource;

    [Header("SFX")]
    [SerializeField] private bool _isSFXEnabled;
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

        _audioClipsDict = new Dictionary<string, AudioClip>();

        foreach (var entry in audioClipEntries)
        {
            if (!_audioClipsDict.ContainsKey(entry.sceneName))
            {
                _audioClipsDict.Add(entry.sceneName, entry.audioClip);
            }
        }
    }

    public void IsMusicEnabled(bool enable)
    {
        _isMusicEnabled = enable;
    }

    public void IsSFXEnabled(bool enable)
    {
        _isSFXEnabled = enable;
    }

    private void Update()
    {
        if (_isMusicEnabled)
        {
            _backgroundMusicSource.volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.Volume, 0.5f);
        }
        else
        {
            _backgroundMusicSource.volume = 0f;
        }

        if (_isMusicEnabled)
        {
            _sfxSource.volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.Volume, 0.5f);
        }
        else
        {
            _sfxSource.volume = 0f;
        }
    }

    public void PlayBackgroundMusic(string scene)
    {
        if (_audioClipsDict.TryGetValue(scene, out var clip))
        {
            _backgroundMusicSource.clip = clip;
            _backgroundMusicSource.Play();
        }
    }
}
