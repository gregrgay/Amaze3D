using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlipCatPhoto : MonoBehaviour
{
    public float x, y, z;
    public GameObject catTextPanel;
    public bool catTextPanelIsActive;
    public int timer;
    public bool interactable;
    public GameObject aButton;

    void Start()
    {
        catTextPanelIsActive = false;
        interactable = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {

            if (interactable == true)
            {
                StartFlip();
                interactable = false;
                StartCoroutine(StopInteraction());
            }

        }
    }

    public void StartFlip()
    {
        StartCoroutine(CalculateFlip());

         
    }

    public void Flip()
    {
        if (catTextPanelIsActive == true)
        {
            catTextPanel.SetActive(false);
            catTextPanelIsActive = false;
        }
        else
        {
            catTextPanel.SetActive(true);
            catTextPanelIsActive = true;
            UAP_AccessibilityManager.Say("F E L I X");
            StartCoroutine(GoToKeypad());

        }
    }
    IEnumerator CalculateFlip()
    {
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(0.005f);
            transform.Rotate(new Vector3(x, y, z));
            timer++;

            if (timer == 90 || timer == -90)
            {
                Flip();                
            }
        }
        timer = 0;
    }
    IEnumerator StopInteraction()
    {
        yield return new WaitForSeconds(5);
        interactable = true;
    }
    IEnumerator GoToKeypad()
    {
        yield return new WaitForSeconds(3);
        EventSystem.current.SetSelectedGameObject(aButton);
    }
}