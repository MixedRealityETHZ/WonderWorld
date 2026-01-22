using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponentToggle : MonoBehaviour
{
    // I recognize enable/disable only can be implemented by just adding the gameobject to the button and setting it to SetActive with the appropriate bool, but for consistency's sake, we're doing it like this, so
    public enum ToggleBehavior
    {
        Toggle,
        EnableOnly,
        DisableOnly
    }

    public GameObject component;
    public ToggleBehavior toggleBehavior = ToggleBehavior.Toggle;
    public void ToggleComponent()
    {
        switch (toggleBehavior)
        {
            case ToggleBehavior.Toggle:
                component.SetActive(!component.activeSelf);
                break;
            case ToggleBehavior.EnableOnly:
                component.SetActive(true);
                break;
            case ToggleBehavior.DisableOnly:
                component.SetActive(false);
                break;
        }
    }

    public void OnHoveredOver()
    {
        Debug.Log("Hovering");
    }
}