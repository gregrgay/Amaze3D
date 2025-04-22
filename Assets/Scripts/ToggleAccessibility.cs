using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleAccessibility : MonoBehaviour
{
    public Toggle toggle;
    public GameObject slider_panel;

    void Start()
    {
        slider_panel = GameObject.Find("Slider");
        if (PlayerPrefs.GetInt("a11ymode") == 1)
        {
            slider_panel.SetActive(false);
            toggle.isOn = false;
        }
        else
        {
            UAP_AccessibilityManager.EnableAccessibility(true, true);
            slider_panel.SetActive(true);
            toggle.isOn = true;
        }

        // Set UAP SpeechRate based on user input from the menu screen
        // Set in the SliderValue() class, set to 95% by default if not already set.
        if (PlayerPrefs.GetInt("Accessibility_Speech_Rate") >= 1)
        {
            SliderValue.SpeechRate = PlayerPrefs.GetInt("Accessibility_Speech_Rate");
            Debug.Log("Set existing" + SliderValue.SpeechRate);
        }
        
        UAP_AccessibilityManager.SetSpeechRate(Mathf.RoundToInt(SliderValue.SpeechRate));

    }

    public void Toggle_Changed(bool newValue)
    {
        if (toggle.isOn == false)
        {
            UAP_AccessibilityManager.Say("Accessibility Mode off. Press Spacebar to navigate buttons with your keyboard.");
            slider_panel.SetActive(false);
            StartCoroutine(PausetoRead());

        }
        else if (toggle.isOn == true)
        {
            PlayerPrefs.DeleteKey("a11ymode");
            PlayerPrefs.Save();
            slider_panel.SetActive(true);
            UAP_AccessibilityManager.EnableAccessibility(true, true);

        }
    }
    IEnumerator PausetoRead()
    {
        yield return new WaitForSeconds(5.5f);
        UAP_AccessibilityManager.EnableAccessibility(false, true);
        PlayerPrefs.SetInt("a11ymode", 1);
        PlayerPrefs.Save();
    }
}
