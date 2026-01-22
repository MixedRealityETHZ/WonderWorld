using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public Window window;

    public void EnableWindow()
    {
        UIManager.Instance.EnableWindow(window);
    }

    public void DisableWindow()
    {
        UIManager.Instance.DisableAll();
    }
}
