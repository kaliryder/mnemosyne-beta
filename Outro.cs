using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Unity.VisualScripting;

public class Outro : MonoBehaviour
{
    public MVideo video;
    public float videoSeconds;
    public GameObject menuPanel;
    public GameObject videoPanel;
    public Image play;
    public Image skip;
    public Image complete;
    public Button playButton;
    public Button skipButton;
    public Button completeButton;

    void Awake() {
        ShowLayout(1);
        videoPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => {
            StartCoroutine(PlayVideoWithDelay());
        });

        skipButton.onClick.AddListener(() => {
            EndVideo();
        });
        completeButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Menu");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayVideoWithDelay()
    {
        ShowLayout(2);

        videoPanel.SetActive(true);
        video.Show(true);
        video.PlayVideo();

        yield return new WaitForSeconds(videoSeconds);

        // The video has finished playing
        EndVideo();
    }

    public void EndVideo() {
        videoPanel.SetActive(false);
        video.Show(false);

        ShowLayout(3);
    }

    private void ClearLayout() {
        play.enabled = false;
        playButton.gameObject.SetActive(false);
        playButton.interactable = false;
        skip.enabled = false;
        skipButton.gameObject.SetActive(false);
        skipButton.interactable = false;
        complete.enabled = false;
        completeButton.gameObject.SetActive(false);
        completeButton.interactable = false;
        menuPanel.SetActive(false);
    }

    private void ShowLayout(int num) {
        ClearLayout();
        menuPanel.SetActive(true);
        if(num == 1) {
            play.enabled = true;
            playButton.gameObject.SetActive(true);
            playButton.interactable = true;
        }
        else if(num == 2) {
            skip.enabled = true;
            skipButton.gameObject.SetActive(true);
            skipButton.interactable = true;
        }
        else if(num == 3) {
            complete.enabled = true;
            completeButton.gameObject.SetActive(true);
            completeButton.interactable = true;
        }
    }
}