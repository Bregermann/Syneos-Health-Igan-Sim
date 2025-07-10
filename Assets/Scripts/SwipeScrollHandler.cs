using UnityEngine;

public class SwipeScrollHandler : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isSwiping = false;
    public float swipeThreshold = 50f; // Minimum swipe distance to trigger

    public TimelineController timelineController;

    public bool canSwipeForward;
    public bool canSwipeBackward;

    void Update()
    {
        HandleTouchInput();
        HandleMouseScroll();

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (canSwipeBackward == true)
            {
                timelineController.PlayReverse();
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (canSwipeForward == true)
            {
                timelineController.PlayForward();
            }
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        float deltaY = touch.position.y - touchStartPos.y;
                        if (Mathf.Abs(deltaY) > swipeThreshold)
                        {
                            if (deltaY > 0)
                                //OnSwipeUp();
                                OnSwipeDown();
                            else
                                OnSwipeUp();
                                //OnSwipeDown();
                            isSwiping = false;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    isSwiping = false;
                    break;
            }
        }
    }

    void HandleMouseScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
            OnScrollUp();
        else if (scroll < 0)
            OnScrollDown();
    }

    void OnSwipeUp()
    {
        //Debug.Log("Swiped Up");
        // Add your action here
        if (canSwipeBackward == true)
        {
            timelineController.PlayReverse();
        }
        
    }

    void OnSwipeDown()
    {
        //Debug.Log("Swiped Down");
        // Add your action here
        if (canSwipeForward == true)
        {
            timelineController.PlayForward();
        }
        
    }

    void OnScrollUp()
    {
        //Debug.Log("Scrolled Up");
        // Add your action here
        if (canSwipeBackward == true)
        {
            timelineController.PlayReverse();
        }

    }

    void OnScrollDown()
    {
        //Debug.Log("Scrolled Down");
        // Add your action here
        if (canSwipeForward == true)
        {
            timelineController.PlayForward();
        }
    }

    public void ToggleForwardSwipeON()
    {
        canSwipeForward = true;
    }

    public void ToggleForwardSwipeOFF()
    {
        canSwipeForward = false;
    }

    public void ToggleBackwardSwipeON()
    {
        canSwipeBackward = true;
    }

    public void ToggleBackwardSwipeOFF()
    {
        canSwipeBackward = false;
    }
}
