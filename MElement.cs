using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MElement : MonoBehaviour
{
    private Gathering gathering; // Reference to the Gathering script
    private AudioSource audioSource;

    void Start()
    {
        // Find the Gathering script in the scene
        gathering = GameObject.FindObjectOfType<Gathering>();
        audioSource = GetComponent<AudioSource>();

        if (gathering == null)
        {
            Debug.LogError("Gathering script not found in the scene.");
        }
    }

    void Update()
    {
        // Your other code here
    }

    public void OnElementHoverEnter()
    {
        // Get the material color from the Gathering script
        Material hoverMaterial = gathering.GetElementHoverMaterial();

        AudioClip hoverSound = gathering.GetElementHoverSound();

        // Change the object color using the obtained hover color
        ChangeObjectMaterial(hoverMaterial);

        PlayElementSound(hoverSound);
    }

    public void OnElementHoverExit() {
        Material initialMaterial = gathering.GetElementInitialMaterial();
        ChangeObjectMaterial(initialMaterial);
        StopElementSound();
    }

    public void ChangeObjectMaterial(Material newMaterial)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
        else
        {
            Debug.LogError("The GameObject does not have a Renderer component.");
        }
    }

    public void PlayElementSound(AudioClip soundClip) {
        if(audioSource != null && soundClip != null) {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
        else {
            Debug.LogError("AudioSource or AudioClip is missing.");
        }
    }

    public void StopElementSound() {
        audioSource.Stop();
    }
}
