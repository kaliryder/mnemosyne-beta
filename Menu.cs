using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Intro");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
