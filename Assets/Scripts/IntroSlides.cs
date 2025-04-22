using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSlides : MonoBehaviour
{
    public static int introCount = 0;
    public TextBoxManager textBoxManager;
    public GameObject intro_01;
    public GameObject intro_02;
    public GameObject intro_03;
    public GameObject intro_04;
    public GameObject intro_05;
    public GameObject intro_06;
    public GameObject intro_07;
    public GameObject intro_08;
    public GameObject intro_09;
    public LoadLevel1 loadLevel1;
    public UI_Inventory uiInventory;
    public Inventory inventory;


    public enum Slides
    {
        intro_01,
        intro_02,
        intro_03,
        intro_04,
        intro_05,
        intro_06,
        intro_07,
        intro_08,
        intro_09,
    }

    private void Start()
    { 
        intro_02.SetActive(false);
        intro_03.SetActive(false);
        intro_04.SetActive(false);
        intro_05.SetActive(false);
        intro_06.SetActive(false);
        intro_07.SetActive(false);
        intro_08.SetActive(false);
        intro_09.SetActive(false);
        textBoxManager.ApplyText(0);
    }

    void Update()
    {

        if (Input.GetKeyUp("escape"))
        {
            UAP_AccessibilityManager.StopSpeaking();
        }

    }
    public void RotateSlides()
    {
        if (Input.GetMouseButtonDown(0) ||
                 Input.GetKeyDown("space") ||
                 Input.GetKeyDown("return"))
        {

            loadLevel1 = GetComponent<LoadLevel1>();

            switch (introCount)
            {
                default:
                case 0: intro_01.SetActive(true); textBoxManager.ApplyText(0); break;
                case 1: intro_02.SetActive(true); intro_01.SetActive(false); textBoxManager.ApplyText(1); break;
                case 2: intro_03.SetActive(true); intro_02.SetActive(false); textBoxManager.ApplyText(2); break;
                case 3: intro_04.SetActive(true); intro_03.SetActive(false); textBoxManager.ApplyText(3); break;
                case 4: intro_05.SetActive(true); intro_04.SetActive(false); textBoxManager.ApplyText(4); break;
                case 5: intro_06.SetActive(true); intro_05.SetActive(false); textBoxManager.ApplyText(5); break;
                case 6: intro_07.SetActive(true); intro_06.SetActive(false); textBoxManager.ApplyText(6); break;
                case 7: intro_08.SetActive(true); intro_07.SetActive(false); textBoxManager.ApplyText(7); break;
                case 8: intro_09.SetActive(true); intro_08.SetActive(false); textBoxManager.ApplyText(8); break;
                case 9: loadLevel1.LoadA(); break;

            }
            introCount += 1;
        }
    }
}
