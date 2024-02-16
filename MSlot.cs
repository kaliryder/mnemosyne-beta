using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class MSlot : MonoBehaviour
{
    public int elementNumber;
    private bool isSlotted = false;
    public TMP_Text slotDebugLogText;
    private string slotDebugLogString;
    public MArrangeElement element;
    //public Vector3 elementPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slotDebugLogText.text = slotDebugLogString;
    }

    private void OnCollisionEnter(Collision collision)
    {
        slotDebugLogString += "Collision detected!";

        string concat = "Element" + elementNumber.ToString();

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag(concat))
            {
                slotDebugLogString += "This element collision detected";

                isSlotted = true;

                slotDebugLogString += "Is slotted!";

                element.SetSlotPosition(this.transform.position);
                element.CanHover(false);
                element.SetSlot();

                break;
            }
        }
    }

    public bool GetSlotted() {
        return isSlotted;
    }
}
