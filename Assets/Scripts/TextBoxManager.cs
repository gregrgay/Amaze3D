using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TextBoxManager : MonoBehaviour
{
    public Text theText;
    public TextAsset textFile;
    public TextAsset outroFile;
    public string[] textLines;
    public int currentLine;
    public int endAtLine;
    public GameObject nextButton;

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Intro" && textFile != null)
        {
            textLines = (textFile.text.Split("\n"));


        } else if(SceneManager.GetActiveScene().name == "Outro" && outroFile != null)
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
            StartCoroutine(PausetoMoveFocus());
            UAP_AccessibilityManager.Say(textLines[0], true);
        }

    }

    public void ApplyText(int introCount)
    {

        if ((Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown("space") ||
            Input.GetKeyDown("return")) && introCount > 0)
        {

            theText.text = textLines[introCount];
            if (introCount != 0)
            {
                EventSystem.current.SetSelectedGameObject(nextButton);
                UAP_AccessibilityManager.Say(theText.text, true);

            }

        }

    }
    IEnumerator PausetoMoveFocus()
    {
        yield return new WaitForSeconds(1.0f);
        EventSystem.current.SetSelectedGameObject(nextButton);
    }
}
