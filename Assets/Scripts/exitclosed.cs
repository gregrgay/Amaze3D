using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitclosed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UAP_AccessibilityManager.Say("This door is opened with a remote lever.");
    }
}
