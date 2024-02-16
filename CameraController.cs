using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;  // Reference to your main camera
    private bool isElementTableActive = true;

    void Start()
    {
        // Ensure the camera is initially set up for the element table view
        SetCameraRotation();
    }

    void Update()
    {

    }

    void OnCameraToggle() {
        isElementTableActive = !isElementTableActive;

        // Rotate the camera by 180 degrees to switch views
        SetCameraRotation();
    }

    void SetCameraRotation()
    {
        // Set the camera rotation based on the current view
        if (isElementTableActive)
        {
            mainCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);  // Default rotation for element table view
        }
        else
        {
            mainCamera.transform.rotation = Quaternion.Euler(0f, 180f, 0f);  // Rotate 180 degrees for the machine view
        }
    }
}

