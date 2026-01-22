using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject needle;
    public GameObject mapMenu;

    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapManager>();
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
    }

    public void HighlightPaintingOnMap(Vector2 coordinates)
    {
        mapMenu.SetActive(true);
        needle.SetActive(true);
        needle.transform.localPosition = coordinates;
    }
}
