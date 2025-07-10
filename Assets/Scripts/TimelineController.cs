using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public float playbackSpeed = 1f;

    public Button forwardButton;
    public Button backButton;


    [System.Serializable]
    public class TimelineSignal
    {
        public double signalTime; // Time when signal should trigger
        public UnityEvent onSignalTriggered; // Event to invoke at signal time
        public bool pauseOnTrigger = false; // Option to pause on trigger
    }

    public TimelineSignal[] timelineSignals;

    private double previousTime;
    public bool isReversing = false;
    private Coroutine reverseCoroutine;

    public TrackingHandler trackingHandler;

    public UIAnimation[] uIAnimations;

    public GameObject popUpImage;

    public float unlockTimerVal;

    public bool canProgress;


    void Update()
    {
        if (!isReversing)
        {
            DetectForwardSignals();
        }
    }

    public void SkipToChapter()
    {
        playableDirector.Stop();
        playableDirector.time = 8F;
        playableDirector.Evaluate();
        playableDirector.Play();

        forwardButton.interactable = true;
        backButton.gameObject.SetActive(true);
        backButton.interactable = true;

        //trackingHandler.CallJSWithMessage("skipto_id_4");
    }

    public void SkipToTop()
    {
        playableDirector.Stop();
        playableDirector.time = 0F;
        playableDirector.Evaluate();
        playableDirector.Play();

        forwardButton.interactable = true;
        backButton.gameObject.SetActive(false);
        backButton.interactable = false;

        //trackingHandler.CallJSWithMessage("skipto_id_0");
    }

    public void PlayForward()
    {
        if (canProgress == true)
        {
            canProgress = false;

            //Invoke("RunButtonUnLocks", unlockTimerVal);

            AnimatedUIout();

            StopReverse();
            isReversing = false;
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(playbackSpeed);
            playableDirector.Play();

            backButton.gameObject.SetActive(true);
            forwardButton.interactable = false;
            backButton.interactable = false;
        }
        

        

    }

    public void PlayReverse()
    {
        if (canProgress == true)
        {
            canProgress = false;

            //Invoke("RunButtonUnLocks", unlockTimerVal);

            AnimatedUIout();

            StopReverse();
            isReversing = true;
            playableDirector.Pause();
            reverseCoroutine = StartCoroutine(ReversePlayback());

            forwardButton.interactable = false;
            backButton.interactable = false;
        }
        


    }

    private IEnumerator ReversePlayback()
    {
        while (isReversing && playableDirector.time > 0.01F)
        {
            playableDirector.time -= Time.deltaTime * playbackSpeed;
            playableDirector.Evaluate(); // Manual update since PlayableDirector does not support negative speed

            if (DetectReverseSignals()) // If a signal triggered and requires a pause, stop reversing
            {
                isReversing = false;
                playableDirector.Pause();
                yield break; // Exit the coroutine
            }

            yield return null;
        }

        isReversing = false;
        playableDirector.Pause(); // Pause at the start if reached
    }

    private void StopReverse()
    {
        if (reverseCoroutine != null)
        {
            StopCoroutine(reverseCoroutine);
            reverseCoroutine = null;
        }
    }

    private void DetectForwardSignals()
    {
        double currentTime = playableDirector.time;
        foreach (var signal in timelineSignals)
        {
            if (previousTime < signal.signalTime && currentTime >= signal.signalTime)
            {
                //Debug.Log($"Forward Signal Triggered at {signal.signalTime}");
                signal.onSignalTriggered?.Invoke();
                playableDirector.time = signal.signalTime;
                playableDirector.Evaluate();
                //Invoke("RunButtonUnLocks", unlockTimerVal);
                SendTrackingEvent(Mathf.RoundToInt((float)signal.signalTime)/2);
            }
        }
        previousTime = currentTime;
    }

    private bool DetectReverseSignals()
    {
        double currentTime = playableDirector.time;
        foreach (var signal in timelineSignals)
        {
            if (previousTime > signal.signalTime && currentTime <= signal.signalTime)
            {
                //Debug.Log($"Reverse Signal Triggered at {signal.signalTime}");
                signal.onSignalTriggered?.Invoke();

                if (signal.pauseOnTrigger)
                {
                    playableDirector.time = signal.signalTime;
                    playableDirector.Evaluate();
                    //Invoke("RunButtonUnLocks", unlockTimerVal);
                    SendTrackingEvent(Mathf.RoundToInt((float)signal.signalTime)/2);
                    return true; // Signal triggered and requires a pause

                }
            }
        }
        previousTime = currentTime;
        return false;
    }

    public void RunButtonUnLocks()
    {
        //print("RunningButtonUnlocks");

        forwardButton.interactable = true;
        backButton.interactable = true;

        canProgress = true;
    }

    void SendTrackingEvent(int sectionID)
    {
        string trackingIdToSend = "id_" + sectionID;
        trackingHandler.CallJSWithMessage(trackingIdToSend);
    }

    void AnimatedUIout()
    {
        foreach (var uIAnimation in uIAnimations)
        {
            if (uIAnimation.isAnimatedIn == true)
            {
                uIAnimation.AnimOut();
            }
        }

        popUpImage.SetActive(false);

        

    }

}