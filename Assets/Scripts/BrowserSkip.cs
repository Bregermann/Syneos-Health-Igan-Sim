using UnityEngine;
using UnityEngine.Playables;

public class BrowserSkip : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector_m;

    public TimelineController timelineController_desktop;
    public TimelineController timelineController_mobile;

    public TimelineController currentTimecontroller;

    public bool isMobile;

    public GameObject[] desktopUIobjects;
    public GameObject[] mobileUIobjects;

    public SmoothLookAtInput3D lookAtInput3D;

    public void Start()
    {
        //currentTimecontroller.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ToggleMobile("true");
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            ToggleMobile("false");
        }
    }

    public void ToggleMobile(string incomingDecision)
    {
        
        if (incomingDecision == "true")
        {
            Debug.Log("Toggle Recieved: true");
            isMobile = true;
            currentTimecontroller = timelineController_mobile;
            playableDirector_m.RebuildGraph();
            lookAtInput3D.enabled = false;
            foreach (var desktopUIobject in desktopUIobjects)
            {
                desktopUIobject.SetActive(false);
            }
            foreach (var mobileUIobject in mobileUIobjects)
            {
                mobileUIobject.SetActive(true);
            }
        } else
        {
            Debug.Log("Toggle Recieved: false");
            isMobile = false;
            currentTimecontroller = timelineController_desktop;
            playableDirector.RebuildGraph();
            lookAtInput3D.enabled = true;
            foreach (var desktopUIobject in desktopUIobjects)
            {
                desktopUIobject.SetActive(true);
            }
            foreach (var mobileUIobject in mobileUIobjects)
            {
                mobileUIobject.SetActive(false);
            }
        }

        currentTimecontroller.gameObject.SetActive(true);
    }

    public void RemoteSkipToChapter()
    {
        Debug.Log("Remote Skip Recieved");
        currentTimecontroller.SkipToChapter();
    }

    public void RemoteSkipToTop()
    {
        Debug.Log("Remote Skip to Top Recieved");
        currentTimecontroller.SkipToTop();
    }
}
