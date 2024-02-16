using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class MVideo : MonoBehaviour
{
    public GameObject video;
    public VideoPlayer videoPlayer;

    void Awake()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
    }

    public void Show(bool toggle) {
        video.SetActive(toggle);
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }
}


