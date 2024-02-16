using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Arranging : MonoBehaviour
{
    public MArrangeElement[] elements;
    public MSlot[] slots;
    private int slotCounter = 0;
    private bool isSlotted = false;
    public TMP_Text slotCounterText;

    public TMP_Text debugLogText;
    private string debugLogString = "";

    public GameObject machine;
    private Material originalMaterial;
    public Material flashMaterial; // Material for flashing effect
    public float flashDuration = 0.5f; // Duration of the flash effect

    public GameObject menuPanel;
    public Image arranging1;
    public Image arranging2;
    public Image arranging3;
    public Button arrangingButton1;
    public Button arrangingButton2;
    public Button arrangingButton3;

    void Awake()
    {
        ShowLayout(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        arrangingButton1.onClick.AddListener(() => {
            ShowLayout(2);
        });
        arrangingButton2.onClick.AddListener(() => {
            ClearLayout();
        });
        arrangingButton3.onClick.AddListener(() => {
            SceneManager.LoadScene("Outro");
        });

        Renderer machineRenderer = machine.GetComponent<Renderer>();
        originalMaterial = machineRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        debugLogText.text = debugLogString;

        CheckSlotted();

        if (isSlotted)
        {
            ShowLayout(3);
        }

        slotCounterText.text = slotCounter.ToString();
    }

    public void CheckSlotted()
    {
        bool tracker = true;
        int counter = 0;
        foreach (MSlot slot in slots)
        {
            if (!slot.GetSlotted())
            {
                tracker = false;
            }
            else
            {
                counter++;
            }
        }
        if (slotCounter != counter)
        {
            StartCoroutine(FlashMachineTo());
            debugLogString += "slotCounter: " + slotCounter.ToString();
        }
        slotCounter = counter;
        isSlotted = tracker;

        if (isSlotted && slotCounter == slots.Length)
        {
            // All elements slotted, turn the machine permanently
            StartCoroutine(TurnMachinePermanent());
        }
    }

    IEnumerator FlashMachineTo()
    {
        float elapsedTime = 0f;
        Renderer machineRenderer = machine.GetComponent<Renderer>();

        while (elapsedTime < flashDuration)
        {
            float t = elapsedTime / flashDuration;

            // Interpolate between start material and target material
            machineRenderer.material.Lerp(originalMaterial, flashMaterial, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the material is set to the final target material
        machineRenderer.material = flashMaterial;
        StartCoroutine(FlashMachineFrom());
    }

    IEnumerator FlashMachineFrom()
    {
        float elapsedTime = 0f;
        Renderer machineRenderer = machine.GetComponent<Renderer>();

        while (elapsedTime < flashDuration)
        {
            float t = elapsedTime / flashDuration;

            // Interpolate between start material and target material
            machineRenderer.material.Lerp(flashMaterial, originalMaterial, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the material is set to the final target material
        machineRenderer.material = originalMaterial;
    }

    IEnumerator TurnMachinePermanent()
    {
        float elapsedTime = 0f;
        Renderer machineRenderer = machine.GetComponent<Renderer>();

        while (elapsedTime < flashDuration)
        {
            float t = elapsedTime / flashDuration;

            // Interpolate between start material and target material
            machineRenderer.material.Lerp(originalMaterial, flashMaterial, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the material is set to the final target material
        machineRenderer.material = flashMaterial;
    }

    private void ClearLayout() {
        arranging1.enabled = false;
        arrangingButton1.gameObject.SetActive(false);
        arrangingButton1.interactable = false;
        arranging2.enabled = false;
        arrangingButton2.gameObject.SetActive(false);
        arrangingButton2.interactable = false;
        arranging3.enabled = false;
        arrangingButton3.gameObject.SetActive(false);
        arrangingButton3.interactable = false;
        menuPanel.SetActive(false);
    }

    private void ShowLayout(int num) {
        ClearLayout();
        menuPanel.SetActive(true);
        if(num == 1) {
            arranging1.enabled = true;
            arrangingButton1.gameObject.SetActive(true);
            arrangingButton1.interactable = true;
        }
        else if(num == 2) {
            arranging2.enabled = true;
            arrangingButton2.gameObject.SetActive(true);
            arrangingButton2.interactable = true;
        }
        else if(num == 3) {
            arranging3.enabled = true;
            arrangingButton3.gameObject.SetActive(true);
            arrangingButton3.interactable = true;
        }
    }
}
