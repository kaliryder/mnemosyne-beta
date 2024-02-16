using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MArrangeElement : MonoBehaviour
{
    private Arranging arranging; // reference to Arranging script
    private AudioSource audioSource;
    public Material initialMaterial;
    public Material hoverMaterial;
    public AudioClip hoverSound;
    public float slotVolume;

    public float pulseDuration = 1f; // Duration of one pulse cycle
    public float pulseScaleFactor = 1.2f; // Scale factor during pulsation
    public float fadeDuration = 1f; // Duration of fade-in and fade-out

    private Vector3 originalScale; // Original scale of the sphere
    private bool isPulsating = false; // Flag to track pulsation state
    private Renderer rend;
    private bool canHover = true;
    private Vector3 slotPosition;

    void Start()
    {
        // Find the Gathering script in the scene
        arranging = GameObject.FindObjectOfType<Arranging>();
        audioSource = GetComponent<AudioSource>();

        if (arranging == null)
        {
            Debug.LogError("Arranging script not found in the scene.");
        }

        originalScale = transform.localScale;

        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Your other code here
    }

    public void OnElementHoverEnter()
    {
        if(canHover) {
            // Change the object color using the obtained hover color
            StartCoroutine(FadeInMaterial());
            //ChangeObjectMaterial(hoverMaterial);

            audioSource.clip = hoverSound;
            audioSource.Play();
            StartCoroutine(FadeInAudio());
            //PlayElementSound(hoverSound);

            if (!isPulsating)
            {
                StartCoroutine(Pulsate());
            }
        }
    }

    public void OnElementHoverExit() {
        if(canHover) {
            StartCoroutine(FadeOutMaterial());
            //ChangeObjectMaterial(initialMaterial);

            StartCoroutine(FadeOutAudio());
            audioSource.Stop();
            //StopElementSound();

            // Stop pulsating when mouse exits
            StopAllCoroutines();
            transform.localScale = originalScale;
            audioSource.volume = 0f; // Ensure the volume is set to the final value
            rend.material = initialMaterial; // Ensure the material is set to the final value
            isPulsating = false;
        }
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

    // Low latency sound interactions
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

    IEnumerator Pulsate()
    {
        isPulsating = true;

        while (true)
        {
            float t = Mathf.PingPong(Time.time / pulseDuration, 1f); // Ping-pong between 0 and 1
            float scale = Mathf.Lerp(originalScale.x, originalScale.x * pulseScaleFactor, t);

            transform.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }
    }

    private IEnumerator FadeInAudio() {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f; // Ensure the volume is set to the final value
    }

    private IEnumerator FadeOutAudio()
    {
        float timer = 0f;
        float initialVolume = audioSource.volume;

        while (timer < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(initialVolume, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f; // Ensure the volume is set to the final value
    }

    private IEnumerator FadeInMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            float timer = 0f;
            Material initialMaterialCopy = new Material(initialMaterial); // Create a copy to avoid modifying the original

            while (timer < fadeDuration)
            {
                renderer.material.Lerp(initialMaterialCopy, hoverMaterial, timer / fadeDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            renderer.material = hoverMaterial; // Ensure the material is set to the final value
        }
        else
        {
            Debug.LogError("The GameObject does not have a Renderer component.");
        }
    }

    private IEnumerator FadeOutMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            float timer = 0f;
            Material hoverMaterialCopy = new Material(hoverMaterial); // Create a copy to avoid modifying the original

            while (timer < fadeDuration)
            {
                renderer.material.Lerp(hoverMaterialCopy, initialMaterial, timer / fadeDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            renderer.material = initialMaterial; // Ensure the material is set to the final value
        }
        else
        {
            Debug.LogError("The GameObject does not have a Renderer component.");
        }
    }

    public void SetSlot() {
        transform.localScale = originalScale;
        audioSource.volume = slotVolume;
        rend.material = hoverMaterial;
        this.transform.position = slotPosition;
        isPulsating = true;
    }

    public void CanHover(bool toggle) {
        canHover = toggle;
    }

    public void SetSlotPosition(Vector3 slotPosition) {
        this.slotPosition = slotPosition;
    }

}

