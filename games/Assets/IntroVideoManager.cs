using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroVideoManager : MonoBehaviour
{
    public RenderTexture videoTexture; // Reference to a RenderTexture
    public RawImage videoDisplay; // Reference to the RawImage component

    public Canvas videos; // Reference to the Canvas object
    public VideoPlayer introVideoPlayer; // Reference to the VideoPlayer object

    void Start()
    {
        videoTexture = new RenderTexture(Screen.width, Screen.height, (int)RenderTextureFormat.ARGB32); // Create a RenderTexture with screen resolution
        videoDisplay.texture = videoTexture; // Set the RawImage's texture to the RenderTexture

        introVideoPlayer.prepareCompleted += OnVideoPrepared; // Add listener for video preparation
        introVideoPlayer.targetTexture = videoTexture; // Set the RenderTexture as the target for video playback
        introVideoPlayer.Prepare(); // Start preparing the video
    }

    void OnVideoPrepared(VideoPlayer videoPlayer)
    {
        Debug.Log("Video is prepared for playback");
        // This function gets called when the video is prepared for playback
        introVideoPlayer.Play(); // Play the video when prepared
    }

    void Update()
    {
        if (!introVideoPlayer.isPlaying)
        {
            videoDisplay.enabled = true; // Enable the RawImage component

            Invoke(nameof(DisableVideoDisplay), 8f); // Invoke the DisableVideoDisplay method after 8 seconds
        }
    }

    void DisableVideoDisplay()
    {
        videoDisplay.enabled = false;
        videos.enabled = false; // Disable the RawImage component
    }
}
