using GaussianSplatting.Runtime;
using Oculus.Interaction.Locomotion;
using UnityEngine;

[RequireComponent(typeof(GaussianSplatRenderer))]
public class SplatsManager : MonoBehaviour
{
    public GameObject passthroughAccessComponent;
    public GaussianSplatRenderer splatRenderer;
    public Transform centerEyeAnchor;

    private static SplatsManager _instance;
    public static SplatsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SplatsManager>();
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
        splatRenderer.enabled = false;
    }

    public void ShowSplats(GaussianSplatAsset splat)
    {
        // set the splats to be at the headset position
        Vector3 newPosition = centerEyeAnchor.position;
        newPosition.y -= 0.5f;
        transform.position = newPosition;
        transform.localEulerAngles = new(0, 180 + centerEyeAnchor.localEulerAngles.y, 0);

        // Reset the splat asset
        splatRenderer.m_Asset = splat;

        // turn on the renderer
        splatRenderer.enabled = true;

        // turn off passthrough
        passthroughAccessComponent.SetActive(false);
    }

    public void CloseSplats()
    {
        // turn off the renderer
        splatRenderer.enabled = false;

        // turn on passthrough
        passthroughAccessComponent.SetActive(true);
    }
}
