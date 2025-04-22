using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TheEndControl : MonoBehaviour
{
    public GameObject buttonCanvas;
    public GameObject theEnd;
    public EventSystem eventSystem;

    void Start()
    {
        theEnd.SetActive(false);
    }

    void Update()
    {
        buttonCanvas.SetActive(false);
    }

    public void OpenURL()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSe-iD9PO5xRfSQbuHj39_kKIea7ZOJ6Tv-Wm03kUMsLac5Tvw/viewform?vc=0&c=0&w=1&usp=mail_form_link");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Game Menu");
    }
    public void GetGuide()
    {
        Application.OpenURL("https://de.ryerson.ca/games/accessibility/_/accessibility_guidelines.pdf?");
    }
}
