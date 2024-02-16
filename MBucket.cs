using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MBucket : MonoBehaviour
{
    private int elementCounter = 0;
    private bool isGathered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Element")) {
            elementCounter++;

            if(elementCounter == 7) {
                isGathered = true;
            }
        }
    }

    public bool IsGathered() {
        return isGathered;
    }

    public int GetGatheredCount() {
        return elementCounter;
    }
}
