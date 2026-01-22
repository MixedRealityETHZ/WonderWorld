using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    Dictionary<int, Painting> paintings = new();
    public GameObject qrPrompt;
    int scanned = -1;

    private static CollectionManager _instance;
    public static CollectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CollectionManager>();
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

    void Start()
    {
        Debug.Log("THIS IS THE COLLECTION MANAGER");

        Painting[] available = GetComponentsInChildren<Painting>();
        foreach (Painting painting in available)
        {
            paintings.Add(painting.Id, painting);
        }

        Debug.Log("There are " + paintings.Count + "paintings available");

        gameObject.SetActive(false);
    }

    public void QRPrompt(int identifier)
    {
        if (paintings.ContainsKey(identifier))
        {
            scanned = identifier;
            qrPrompt.SetActive(true);
        }
    }

    public void OpenInfo()
    {
        Debug.Log("Attempting to open info");
        paintings[scanned].OpenInfoMenu(true);
        qrPrompt.SetActive(false);
    }
}
