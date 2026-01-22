using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GaussianSplatting.Runtime;

public class PaintingInfoUIManager : MonoBehaviour
{
    public RawImage image;
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _artist;
    public TextMeshProUGUI _medium;
    public TextMeshProUGUI _info;
    public GameObject backButton;
    public GameObject splatsButton;
    Vector2 _coordinates;
    GaussianSplatAsset _splat;
    public MapManager mapManager;
    public VoiceoverPlayer voiceoverPlayer;

    bool showingSplats = false;
    public bool ShowingSplats => showingSplats;

    private static PaintingInfoUIManager _instance;
    public static PaintingInfoUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PaintingInfoUIManager>();
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
        Debug.Log("Painting UI manager");
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        UIManager.Instance.EnableWindow(Window.PaintingInfo);
    }

    public void ShowPanoramaButton(bool value)
    {
        splatsButton.SetActive(value);
        backButton.SetActive(!value);
    }

    public void ShowSplats()
    {
        showingSplats = true;
        SplatsManager.Instance.ShowSplats(_splat);
    }
    
    public void DisableSplats()
    {
        if (showingSplats) {
            showingSplats = false;
            UIManager.Instance.EnableWindow(Window.PaintingInfo);
        }
    }
    public void BackToMenu()
    {
        UIManager.Instance.EnableWindow(Window.CollectionMenu);
    }

    public void SetImage(Texture2D texture)
    {
        image.texture = texture;
        image.SetNativeSize();
    }
    public void SetTitle(string title)
    {
        _title.text = title;
    }

    public void SetArtist(string artist)
    {
        _artist.text = artist;

    }

    public void SetMedium(string medium)
    {
        _medium.text = medium;
    }

    public void SetInfo(string info)
    {
        _info.text = info;
    }

    public void SetMapCoordinates(Vector2 coordinates)
    {
        _coordinates = coordinates;
    }

    public void SetSplatAsset (GaussianSplatAsset splatAsset)
    {
        _splat = splatAsset;
    }

    public void SetSoundSource(AudioSource audio)
    {
        voiceoverPlayer.setAudioSource(audio);
    }

    public void HighlightPaintingOnMap()
    {
        if (_coordinates != null)
        {
            mapManager.HighlightPaintingOnMap(_coordinates);
        }
    }
}
