using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lock1Controller : MonoBehaviour
{
    public GameObject puzzle1Canvas;
    public Text lockText;
    public GameObject fpsController;
    public GameObject catPanel;
    public GameObject catTextPanel;
    public GameObject doorClosed;
    public AudioSource[] sounds;
    public AudioSource errornoise;
    public AudioSource doornoise;
    public GameObject doorOpen;
    RotateDoor rotateDoor;
    public AccessibleCamera accessibleCamera;
                                        
    private void Start()
    {
        puzzle1Canvas.SetActive(false);
        catPanel.SetActive(false);
        catTextPanel.SetActive(false);
        fpsController = GameObject.Find("FPSController");
        rotateDoor = GetComponent<RotateDoor>();
        fpsController.GetComponent<FirstPersonController>().enabled = true;
        sounds = puzzle1Canvas.GetComponents<AudioSource>();
        errornoise = sounds[0];
        doornoise = sounds[1];

        accessibleCamera = GameObject.FindWithTag("MainCamera").GetComponent<AccessibleCamera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            puzzle1Canvas.SetActive(false);
            fpsController.GetComponent<FirstPersonController>().enabled = true;
            accessibleCamera.enablePointer();
        }
    }

    public void ClickLetter(string letterClicked)
    {
        lockText.text += letterClicked;
        UAP_AccessibilityManager.Say(lockText.text);
    }

    public void ClearLetter()
    {
        lockText.text = lockText.text.Substring(0, lockText.text.Length - 1);
    }
    public void CheckCombo()
    {
        string combo = "FELIX";
        if (lockText.text == combo)
        {
            // unlock the door
            StartCoroutine(PlayOpeningDoor());
            UAP_AccessibilityManager.Say("Door has opened.");
            //reneable to firstpersoncontroller
            fpsController.GetComponent<FirstPersonController>().enabled = true;
        }
        else
        { // wrong, try again
            errornoise.Play();
            UAP_AccessibilityManager.Say("Wrong combination. Try again.");
        }
    }

    private void OpenDoor3()
    {

        PlayerPrefs.SetInt("door_closed", 1);
        PlayerPrefs.Save();

      //  UAP_AccessibilityManager.Say("Door has opened.");
        puzzle1Canvas.SetActive(false);
        accessibleCamera.enablePointer();

    }

    private void OnTriggerEnter(Collider other)
    {       
        fpsController.GetComponent<FirstPersonController>().enabled = false;
        accessibleCamera.disablePointer();
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