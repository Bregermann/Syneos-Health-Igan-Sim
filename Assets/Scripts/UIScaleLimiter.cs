using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class UIScaleLimiter : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    private CanvasScaler scaler;

    void Awake()
    {
        scaler = GetComponent<CanvasScaler>();
    }

    void Update()
    {
        float currentScaleFactor = CalculateScaleFactor();
        float clampedScale = Mathf.Clamp(currentScaleFactor, minScale, maxScale);
        scaler.scaleFactor = clampedScale;
    }

    float CalculateScaleFactor()
    {
        // This logic mirrors what Unity does internally when using "Scale With Screen Size"
        Vector2 referenceResolution = scaler.referenceResolution;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float scaleX = screenWidth / referenceResolution.x;
        float scaleY = screenHeight / referenceResolution.y;

        float match = scaler.matchWidthOrHeight;
        return Mathf.Lerp(scaleX, scaleY, match);
    }
}
