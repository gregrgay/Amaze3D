using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    public Slider sliderUI;
    private GameObject slider_panel;
    public static float SpeechRate;


    void Start()
    {
        slider_panel = GameObject.FindWithTag("slider1");
        ShowSliderValue();
    }

    public void OnValueChange(int value)
    {


        Text[] textValue;
        textValue = GameObject.Find("Slider").GetComponentsInChildren<Text>();
        textValue[1].text = sliderUI.value.ToString();
        SpeechRate = sliderUI.value;
        PlayerPrefs.SetInt("Accessibility_Speech_Rate", Mathf.RoundToInt(SpeechRate));
        Debug.Log("SpeechRate = "+ SpeechRate);


    }

    public void ShowSliderValue()
    {
        Text[] textValue;
        textValue = GameObject.Find("Slider").GetComponentsInChildren<Text>();

        if (PlayerPrefs.GetInt("Accessibility_Speech_Rate") > 1)
        {
            textValue[1].text = PlayerPrefs.GetInt("Accessibility_Speech_Rate").ToString();
            sliderUI.value = PlayerPrefs.GetInt("Accessibility_Speech_Rate");
            SpeechRate = PlayerPrefs.GetInt("Accessibility_Speech_Rate");
        }
        else
        {
            textValue[1].text = sliderUI.value.ToString();
            SpeechRate = sliderUI.value;
        }

        UAP_AccessibilityManager.SetSpeechRate(Mathf.RoundToInt(SpeechRate));
        Debug.Log("SpeechRate2 = " + SpeechRate);
    }
}