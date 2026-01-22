using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GaussianSplatting.Runtime;
public class Painting : MonoBehaviour
{
    [SerializeField]
    string title;
    public string Title => title;
    [SerializeField]
    string artist;
    public string Artist => artist;
    [SerializeField]
    string medium;
    public string Medium => medium;
    [SerializeField]
    string info;
    public string Info => info;
    [SerializeField]
    Texture2D image;
    public Texture2D Image => image;
    [SerializeField]
    GaussianSplatAsset splat;
    public GaussianSplatAsset Splat => splat;  
    [SerializeField]
    int id;
    public int Id => id;
    [SerializeField]
    Vector2 coordinates;
    public Vector2 Coordinates => coordinates;
    [SerializeField]
    public AudioSource audio; 
  

    void Start()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = title;
        RawImage thumbnail = GetComponentInChildren<RawImage>();
        thumbnail.texture = image;
        thumbnail.SetNativeSize();
    }

    public void OpenInfoMenu(bool withSplats)
    {
        Debug.Log("Open with splats? " + withSplats);
        PaintingInfoUIManager.Instance.Enable();
        //PaintingInfoUIManager.Instance.gameObject.SetActive(true);
        PaintingInfoUIManager.Instance.ShowPanoramaButton(withSplats);
        PaintingInfoUIManager.Instance.SetSplatAsset(splat);
        PaintingInfoUIManager.Instance.SetImage(image);
        PaintingInfoUIManager.Instance.SetTitle(title);
        PaintingInfoUIManager.Instance.SetArtist(artist);
        PaintingInfoUIManager.Instance.SetMedium(medium);
        PaintingInfoUIManager.Instance.SetInfo(info);
        PaintingInfoUIManager.Instance.SetMapCoordinates(coordinates);
        PaintingInfoUIManager.Instance.SetSoundSource(audio);
        // Splat instance, set splat asset
    }
}
