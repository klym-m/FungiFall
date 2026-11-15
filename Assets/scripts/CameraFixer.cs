using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFixer : MonoBehaviour
{
    public float targetAspect = 16f / 9f;

    void Update()
    {
        var cam = GetComponent<Camera>();
        float scale = ((float)Screen.width / Screen.height) / targetAspect;
        if (scale < 1f)
            cam.rect = new Rect(0, (1f - scale) / 2f, 1f, scale);
        else
        {
            float w = 1f / scale;
            cam.rect = new Rect((1f - w) / 2f, 0, w, 1f);
        }
    }
}