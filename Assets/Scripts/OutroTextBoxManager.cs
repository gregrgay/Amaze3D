using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class OutroTextBoxManager : MonoBehaviour
{

    public Text theText;
    public TextAsset outroFile;
    public string[] textLines;
    public int currentLine;
    public int endAtLine;
    public EventSystem eventSystem;
    public GameObject nextButton;

    void Start()
    {

        if(outroFile != null)
        {
            textLines = (outroFile.text.Split("\n"));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if (currentLine == 0 && textLines.Length > 0)
        {
            theText.text = textLines[0];
            currentLine += 1;
            EventSystem.current.SetSelectedGameObject(nextButton);
            StartCoroutine(ReadFirstBubble());
            //UAP_AccessibilityManager.Say(textLines[0], true);
        }

    }
    void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            UAP_AccessibilityManager.StopSpeaking();
        }
    }
    public void ApplyText(int outroCount)
    {
        if ((Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown("space") ||
            Input.GetKeyDown("return")) && outroCount > 0)
        {

            theText.text = textLines[outroCount];

            if (outroCount != 0)
            {
                EventSystem.current.SetSelectedGameObject(nextButton);
                UAP_AccessibilityManager.Say(theText.text, true);
            }

        }

    }

    IEnumerator ReadFirstBubble()
    {
        yield return new WaitForSeconds(3.0f);
        UAP_AccessibilityManager.Say(textLines[0], true);
    }

}
