using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MGame : MonoBehaviour
{
    // Awake is called before Start
    private void Awake()
    {
        // makes MGame persist across game changes
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
