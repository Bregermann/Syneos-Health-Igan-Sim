using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;
using System.Collections;

public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoName;
    private string videoPath;


    void Start()
    {
        StartCoroutine(LoadVideo());
    }

    IEnumerator LoadVideo()
    {
        videoPath = Application.streamingAssetsPath + "/" + videoName;

#if UNITY_WEBGL
        videoPath = Application.streamingAssetsPath + "/" + videoName; // Relative path for WebGL
#endif

        UnityWebRequest request = UnityWebRequest.Get(videoPath);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Failed to load video: " + request.error);
        }
    }
}
