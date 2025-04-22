using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassOpenDoor : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        UAP_AccessibilityManager.Say("Passed through open door.");
    }
}
