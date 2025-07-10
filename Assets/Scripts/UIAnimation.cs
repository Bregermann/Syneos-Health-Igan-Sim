using DG.Tweening;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public RectTransform uiElement;

    public TimelineController timelineController;

    public string direction;

    public float OGheight;

    public bool isAnimatedIn = false;

    private void Start()
    {
        OGheight = uiElement.anchoredPosition.y;
    }
    private void OnEnable()
    {
        AnimIn();
    }

    public void AnimIn()
    {
        isAnimatedIn = true;

        canvasGroup.alpha = 0.0f;
        canvasGroup.DOFade(1, 1).SetDelay(0.5F);

        

        if (timelineController.isReversing == true)
        {
            uiElement.anchoredPosition = new Vector3(0, 50);
            uiElement.DOAnchorPosY(0, 1f).SetDelay(0.5F);
        }
        else
        {
            uiElement.anchoredPosition = new Vector3(0, -50);
            uiElement.DOAnchorPosY(0, 1f).SetDelay(0.5F);
        }

        
    }
    public void AnimOut()
    {
        isAnimatedIn = false;

        canvasGroup.alpha = 1.0f;
        canvasGroup.DOFade(0, 1).SetDelay(0.5F);

        if (timelineController.isReversing == true)
        {
            uiElement.anchoredPosition = new Vector3(0, 0);
            uiElement.DOAnchorPosY(-50, 1f).SetDelay(0.5F);
        }
        else
        {
            uiElement.anchoredPosition = new Vector3(0, 0);
            uiElement.DOAnchorPosY(+50, 1f).SetDelay(0.5F);
        }
    }
}
