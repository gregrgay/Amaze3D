using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisLevelItems : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        if (SliderValue.SpeechRate <= 1)
        {
            SliderValue.SpeechRate = 95;

        }
        else if (PlayerPrefs.GetInt("Accessibility_Speech_Rate") > 1)
        {
            SliderValue.SpeechRate = PlayerPrefs.GetInt("Accessibility_Speech_Rate");
        }

        UAP_AccessibilityManager.SetSpeechRate(Mathf.RoundToInt(SliderValue.SpeechRate));
        if (gameObject.tag == "entrance1")
        {
            UAP_AccessibilityManager.Say("This level has a locked chest, diary, three gems, a key, and a locked door.");
        }
        else if (gameObject.tag == "entrance2")
        {
            UAP_AccessibilityManager.Say("This level has a locked chest, diary, three gems, a secret room, a key, and a locked door.");
        }
        else if (gameObject.tag == "entrance3")
        {
            UAP_AccessibilityManager.Say("This level has a locked chest, diary, three gems, a secret room, a key, a lever to open the locked door.");
        }
        else if (gameObject.tag == "entrance4")
        {
            UAP_AccessibilityManager.Say("This level has a diary, three gems, three secret rooms, a key, and a locked door.");
        }
        else if (gameObject.tag == "entrance5")
        {
            UAP_AccessibilityManager.Say("This level has the professor and a locked door.");
        }
    }
}
