using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToState : MonoBehaviour
{
    public GameObject HowTo2;
    public Text theText;
    public TextAsset howto_text;
    public string[] textLines;


    void Start()
    {
        HowTo2 = GameObject.Find("HowToDialog");
        HowTo2.GetComponentInChildren<Canvas>().enabled = false;

        if (howto_text != null)
        {
            textLines = (howto_text.text.Split("\n"));

            for (int i = 0; i < textLines.Length; i++)
            {
                theText.text += textLines[i]+"\n";
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            UAP_AccessibilityManager.StopSpeaking();
            HowTo2.GetComponentInChildren<Canvas>().enabled = false;
        }

        if ((Input.GetKeyUp(KeyCode.H)))
        {
            HowTo2.GetComponentInChildren<Canvas>().enabled = true;
            theText.text = howto_text.text;
            UAP_AccessibilityManager.Say(theText.text, true);
        }        
    }

    public void ClickHowto()
    {
        HowTo2.GetComponentInChildren<Canvas>().enabled = true;
        theText.text = howto_text.text;
        UAP_AccessibilityManager.Say(theText.text, true);
    }
    
}