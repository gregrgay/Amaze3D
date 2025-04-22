using UnityEngine;
using UnityEngine.UI;

public class ShowObjectives : MonoBehaviour
{
    public GameObject Objectives;
    public Text theText;
    public TextAsset objectivestext;
    public string[] textLines;

    void Start()
    {
        Objectives = GameObject.Find("ObjectivesDialog");
        Objectives.SetActive(false);

        if (objectivestext != null)
        {
            textLines = (objectivestext.text.Split("\n"));

            for (int i = 0; i < textLines.Length; i++)
            {
                theText.text += textLines[i] + "\n";
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Objectives.SetActive(false);
        }

    }
    public void ToggleObjectives()
    {
        Objectives.SetActive(true);
        theText.text = objectivestext.text;
        UAP_AccessibilityManager.Say(theText.text, true);

    }
}
