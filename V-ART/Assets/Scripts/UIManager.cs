using System;
using System.Collections;
using System.ComponentModel;
using Unity.XR.CoreUtils;
using UnityEngine;
using static EnableComponentToggle;
using static OVRPlugin;

[Serializable]
public enum Window
{
    NavigationMenu,
    CollectionMenu,
    PaintingInfo,
    Info
}

public class UIManager : MonoBehaviour
{
    public GameObject navigationMenu;
    public GameObject collectionMenu;
    public GameObject paintingInfo;
    public GameObject info;
    public float distanceToSpawn = .5f;

    public GameObject centerEyeAnchor;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
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
        StartCoroutine(StartupMenu());
    }

    IEnumerator StartupMenu()
    {
        yield return new WaitForSeconds(1);
        EnableWindow(Window.Info);
    }

    public void DisableAll()
    {
        DisableUIComponent(navigationMenu);
        DisableUIComponent(collectionMenu);
        DisableUIComponent(paintingInfo);
        DisableUIComponent(info);
    }

    public void EnableWindow(Window window)
    {
        DisableAll();
        switch (window)
        {
            case Window.NavigationMenu:
                EnableUIComponent(navigationMenu);
                break;
            case Window.CollectionMenu:
                EnableUIComponent(collectionMenu);
                break;
            case Window.PaintingInfo:
                EnableUIComponent(paintingInfo);
                break;
            case Window.Info:
                EnableUIComponent(info);
                break;
        }
    }


    private void DisableUIComponent(GameObject uiComponent)
    {
        uiComponent.SetActive(false);
    }

    private void EnableUIComponent(GameObject uiComponent)
    {
        uiComponent.SetActive(true);
        Transform eye = centerEyeAnchor.transform;

        Vector3 dir = new(eye.forward.x, 0, eye.forward.z);
        uiComponent.transform.position = eye.position + dir * distanceToSpawn;
        uiComponent.transform.localEulerAngles = new(0, eye.transform.localEulerAngles.y, 0);

        Transform canvas = uiComponent.transform.GetChild(0);
        canvas.localPosition = Vector3.zero;
        canvas.localEulerAngles = Vector3.zero;
    }

    
}
