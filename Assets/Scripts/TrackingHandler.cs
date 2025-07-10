using System.Runtime.InteropServices;
using UnityEngine;

public class TrackingHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CallExternalJS(string message);

    public void CallJSWithMessage(string message)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            CallExternalJS(message);
#else
        Debug.Log("WebGL only: Simulating JS call in Editor. Message: " + message);
#endif
    }
}

