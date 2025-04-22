using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class a11yState : MonoBehaviour
{
    public GameObject a11yInfo;
    public Text theText;
    public TextAsset a11ytext;
    public string[] textLines;


    void Start()
    {
        a11yInfo = GameObject.Find("a11yDialog");
        a11yInfo.GetComponentInChildren<Canvas>().enabled = false;

        if (a11ytext != null)
        {
            textLines = (a11ytext.text.Split("\n"));

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
            a11yInfo.GetComponentInChildren<Canvas>().enabled = false;
        }

        if ((Input.GetKeyUp(KeyCode.L)))
        {

            a11yInfo.GetComponentInChildren<Canvas>().enabled = true;
            theText.text = a11ytext.text;
            UAP_AccessibilityManager.Say(theText.text, true);

        }

    }
    public void ClickA11y()
    {
        a11yInfo.GetComponentInChildren<Canvas>().enabled = true;
        theText.text = a11ytext.text;
        UAP_AccessibilityManager.Say(theText.text, true);
    }
    
}