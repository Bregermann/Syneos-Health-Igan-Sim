using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class UIScaleWithLimits : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(1920, 1080);
    public float matchWidthOrHeight = 0.5f; // 0 = width, 1 = height
    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    private CanvasScaler scaler;

    void Awake()
    {
        scaler = GetComponent<CanvasScaler>();

        // Switch to manual scaling mode
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
    }

    void Update()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float scaleX = screenWidth / referenceResolution.x;
        float scaleY = screenHeight / referenceResolution.y;

        float scale = Mathf.Lerp(scaleX, scaleY, matchWidthOrHeight);
        scale = Mathf.Clamp(scale, minScale, maxScale);

        scaler.scaleFactor = scale;
    }
}
