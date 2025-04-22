using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Lock2Controller : MonoBehaviour
{
    public GameObject puzzle2Canvas;
    public Text lockText;        // drag your text object on here
    public GameObject fpsController;
    public GameObject metalPlate;
    public GameObject doorClosed;
    public static int lettercount;
    public UnityEngine.UI.Button button1;
    public UnityEngine.UI.Button button2;
    public UnityEngine.UI.Button button3;
    public UnityEngine.UI.Button button4;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public AudioSource[] sounds;
    public AudioSource errornoise;
    public AudioSource doornoise;
    public GameObject doorOpen;
    RotateDoor rotateDoor;
    public AccessibleCamera accessibleCamera;

    private void Start()
    {
        fpsController = GameObject.Find("FPSController");
        sounds = puzzle2Canvas.GetComponents<AudioSource>();
        errornoise = sounds[0];
        doornoise = sounds[1];
        accessibleCamera = GameObject.FindWithTag("MainCamera").GetComponent<AccessibleCamera>();
    }
    private void Awake()
    {
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
        text1.text = null;
        text2.text = null;
        text3.text = null;
        text4.text = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            puzzle2Canvas.SetActive(false);
            fpsController.GetComponent<FirstPersonController>().enabled = true;
            accessibleCamera.enablePointer();
        }

    }

    public void SetButtonLetters()
    {
         text1.text = "C";
         text2.text = "A";
         text3.text = "D";
         text4.text = "B";
    }
    public void ClickLetter(string letterClicked)
    {
        lettercount = lettercount+1;
        lockText.text += letterClicked;
        UAP_AccessibilityManager.Say(lockText.text);
        CheckCombo();
    }

    public void ClearLetter()
    {
        lockText.text = lockText.text.Substring(0, lockText.text.Length - 1);
    }
    public void CheckCombo()
    {
        string combo = "DCAB";
        if (lettercount == 4) { 
            if (lockText.text == combo)
            {
                // unlock the door
                StartCoroutine(PlayOpeningDoor());

                //reneable to firstpersoncontroller
                fpsController.GetComponent<FirstPersonController>().enabled = true;
            }
            else
            { // wrong, try again
                lockText.text = "ERROR";
                errornoise.Play();
                UAP_AccessibilityManager.Say("Wrong combination, try again");
                lockText.text = "";
                lettercount = 0;
            }
        }
    }
    public void EnableButtons()
    {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        button4.interactable = true;
    }
    private void OpenDoor3()
    {
        puzzle2Canvas.SetActive(false);

        PlayerPrefs.SetInt("door_closed", 1);
        PlayerPrefs.Save();
        //How to make inventory remove item work, on a Don't Destroy inventory from level 1
        //inventory.RemoveItem(new Item { itemType = Item.ItemType.panel_small, amount = 1 });
        UAP_AccessibilityManager.Say("Door has opened.");
    }

    private void OnTriggerEnter(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>().enabled = false;
        accessibleCamera.disablePointer();
        UAP_AccessibilityManager.Say("Use arrow keys to navigate. Use Enter key to activate. Escape key to exit the puzzle.");
    }

    private void OnTriggerExit(Collider other)
    {
        fpsController.GetComponent<FirstPersonController>().enabled = true;
        accessibleCamera.enablePointer();
    }

    IEnumerator PlayOpeningDoor()
    {
        rotateDoor = GameObject.Find("RotateDoor").GetComponent<RotateDoor>();
        doornoise.Play();
        rotateDoor.OpenDoor();
        yield return new WaitForSeconds(3.0f);
        OpenDoor3();
    }
}