using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gathering : MonoBehaviour
{
    public MBucket bucket;

    public MElement[] elements;
    
    public TMP_Text elementCounterText;

    public Material elementInitialMaterial;
    public Material elementHoverMaterial;
    public AudioClip elementHoverSound;

    public GameObject menuPanel;
    public Image gathering1;
    public Image gathering2;
    public Image gathering3;
    public Button gatheringButton1;
    public Button gatheringButton2;
    public Button gatheringButton3;

    void Awake() {
        ShowLayout(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        gatheringButton1.onClick.AddListener(() => {
            ShowLayout(2);
        });
        gatheringButton2.onClick.AddListener(() => {
            ClearLayout();
        });
        gatheringButton3.onClick.AddListener(() => {
            SceneManager.LoadScene("Arranging");
        });
    }

    // Update is called once per frame
    void Update()
    {
        elementCounterText.text = bucket.GetGatheredCount().ToString();

        if(bucket.IsGathered()) {
            ShowLayout(3);
        }
    }

    public Material GetElementHoverMaterial() {
        return elementHoverMaterial;
    }

    public Material GetElementInitialMaterial () {
        return elementInitialMaterial;
    }

    public AudioClip GetElementHoverSound() {
        return elementHoverSound;
    }

    private void ClearLayout() {
        gathering1.enabled = false;
        gatheringButton1.gameObject.SetActive(false);
        gatheringButton1.interactable = false;
        gathering2.enabled = false;
        gatheringButton2.gameObject.SetActive(false);
        gatheringButton2.interactable = false;
        gathering3.enabled = false;
        gatheringButton3.gameObject.SetActive(false);
        gatheringButton3.interactable = false;
        menuPanel.SetActive(false);
    }

    private void ShowLayout(int num) {
        ClearLayout();
        menuPanel.SetActive(true);
        if(num == 1) {
            gathering1.enabled = true;
            gatheringButton1.gameObject.SetActive(true);
            gatheringButton1.interactable = true;
        }
        else if(num == 2) {
            gathering2.enabled = true;
            gatheringButton2.gameObject.SetActive(true);
            gatheringButton2.interactable = true;
        }
        else if(num == 3) {
            gathering3.enabled = true;
            gatheringButton3.gameObject.SetActive(true);
            gatheringButton3.interactable = true;
        }
    }
}
