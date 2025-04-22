using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class ButtonThroughKeySelection : MonoBehaviour
{
    public string key;
    private EventSystem eventSystem;
    public GameObject[] navFields;
    private int selected;
    private int maxArray;
    private GameObject currentSelected;

    void Start()
    {

        selected = 0;
        maxArray = navFields.Length - 1;
        currentSelected = navFields[selected].gameObject;

    }
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            if (selected > 0 )
            {
                selected--;
                currentSelected = navFields[selected].gameObject;
                Debug.Log("Selected: " + currentSelected);
                if (EventSystem.current == null)
                    return;

                EventSystem.current.SetSelectedGameObject(currentSelected);
                if (GetComponent<InputField>() != null)
                {
                    GetComponent<InputField>().Select();
                    GetComponent<InputField>().ActivateInputField();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (selected < maxArray)
            {
                selected++;
                currentSelected = navFields[selected].gameObject;
                Debug.Log("Selected: "+currentSelected);

                if (EventSystem.current == null)
                    return;
                EventSystem.current.SetSelectedGameObject(currentSelected);
                if (GetComponent<InputField>() != null)
                {
                    GetComponent<InputField>().Select();
                    GetComponent<InputField>().ActivateInputField();
                }
                
            } else if (selected == maxArray)
            {
                selected = 0; 

            }
        }
    }

}
