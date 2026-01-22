using UnityEngine;
using TMPro;

public class VoiceoverPlayer : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource source;

    [Header("UI")]
    [SerializeField] private TMP_Text remainingLabel; // countdown
    [SerializeField] private GameObject playIcon;     // shown when NOT playing
    [SerializeField] private GameObject pauseIcon;    // shown when playing

    private static VoiceoverPlayer _instance;
    public static VoiceoverPlayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VoiceoverPlayer>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Set singleton instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        if (!source) source = GetComponent<AudioSource>();
        if (source)
        {
            source.playOnAwake = false;
            source.loop = false;
        }

        UpdateIcons(false);
    }


    public void setAudioSource(AudioSource audioSource)
    {
        source = audioSource;
    }


    private void Update()
    {
        if (!source || !source.clip) return;

        // Countdown
        float remaining = Mathf.Max(0f, source.clip.length - source.time);
        if (remainingLabel) remainingLabel.text = FormatTime(remaining);

        // Icon state
        UpdateIcons(source.isPlaying);

        // Optional: when finished, reset to start so next press restarts
        if (!source.isPlaying && remaining <= 0.02f)
        {
            source.time = 0f;
            UpdateIcons(false);
        }
    }

    // Hook this to your VR button's WhenSelect / OnClick
    public void TogglePlayPause()
    {
        if (!source || !source.clip) return;

        // If at end, restart
        if (!source.isPlaying && source.time >= source.clip.length - 0.02f)
            source.time = 0f;

        if (source.isPlaying)
            source.Pause();
        else
            source.Play();

        UpdateIcons(source.isPlaying);
    }

    public void StopAndReset()
    {
        if (!source) return;
        source.Stop();
        source.time = 0f;
        UpdateIcons(false);

        if (remainingLabel && source.clip)
            remainingLabel.text = FormatTime(source.clip.length);
    }

    private void OnDisable()
    {
        // If the window gets deactivated, stop voiceover automatically
        StopAndReset();
    }


    private void UpdateIcons(bool isPlaying)
    {
        if (playIcon) playIcon.SetActive(!isPlaying);
        if (pauseIcon) pauseIcon.SetActive(isPlaying);
    }

    private static string FormatTime(float seconds)
    {
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds % 60f);
        return $"{m:00}:{s:00}";
    }
}
