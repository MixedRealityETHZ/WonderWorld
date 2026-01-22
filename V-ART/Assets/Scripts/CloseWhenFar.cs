using UnityEngine;

public class CloseWhenFar : MonoBehaviour
{
    public Transform centerEyeAnchor;
    public UIToggle uIToggle;
    public float thresholdDistance = 2f;

    void Update()
    {
        if (Vector3.Distance(centerEyeAnchor.position, transform.position) > thresholdDistance)
        {
            uIToggle.DisableWindow();
        }
    }
}
