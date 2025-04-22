using UnityEngine;
using UnityEngine.EventSystems;

public class OutroSlides : MonoBehaviour
{
    public static int outroCount = 0;
    public OutroTextBoxManager outroTextBoxManager;
    public GameObject outro_01;
    public GameObject outro_02;
    public GameObject outro_03;
    public GameObject theEnd;
    //public GameObject aButton;


    public enum Slides
    {

        outro_01,
        outro_02,
        outro_03,
    }

    private void Start()
    {

        if(theEnd != null)
        {
            theEnd.SetActive(false);
        }

        outro_02.SetActive(false);
        outro_03.SetActive(false);
        outroTextBoxManager.ApplyText(0);
        //aButton.SetSelectedGameObject();
    }
    public void RotateSlides()
    {
        if (Input.GetMouseButtonDown(0) ||
                 Input.GetKeyDown("space") ||
                 Input.GetKeyDown("return"))
        {

            switch (outroCount)
            {
                default:
                case 0: outro_01.SetActive(true); outroTextBoxManager.ApplyText(0); break;
                case 1: outro_02.SetActive(true); outro_01.SetActive(false); outroTextBoxManager.ApplyText(1); break;
                case 2: outro_03.SetActive(true); outro_02.SetActive(false); outroTextBoxManager.ApplyText(2); break;
                case 3: outro_03.SetActive(true); outroTextBoxManager.ApplyText(3); break;
                case 4: outro_03.SetActive(true); outroTextBoxManager.ApplyText(4); break;
                case 5: theEnd.SetActive(true); outro_03.SetActive(false); break; 
            }

            outroCount += 1;
        }
    }
}
