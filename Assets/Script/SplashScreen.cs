using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public GameObject videoPlayer;
    public GameObject canvas;
    void Awake()
    {
        //videoPlayer.GetComponent<VideoPlayer>().aspectRatio = VideoAspectRatio.FitOutside;
        canvas.SetActive(false);
        videoPlayer.SetActive(true);
        videoPlayer.GetComponent<VideoPlayer>().Play();
        videoPlayer.SetActive(true);
        StartCoroutine(ShowNextScreen());
    }
    IEnumerator ShowNextScreen()
    {
        yield return new WaitForSeconds((float)videoPlayer.GetComponent<VideoPlayer>().length);
        videoPlayer.SetActive(false);
        canvas.SetActive(true);
    }
}
