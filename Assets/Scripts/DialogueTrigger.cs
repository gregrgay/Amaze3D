using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public GameObject Dialog_Panel;
    public int bumpcount;
    public GameObject fpsController;
    public AccessibleCamera accessibleCamera;

    private void Start()
    {
        fpsController = GameObject.Find("FPSController");
        accessibleCamera = GameObject.FindWithTag("MainCamera").GetComponent<AccessibleCamera>();

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) &&  Dialog_Panel.activeInHierarchy == true) {
            Dialog_Panel.SetActive(false);
            UAP_AccessibilityManager.StopSpeaking();
            UAP_AccessibilityManager.Say("Closed dialog");
            if (accessibleCamera)
            {
                accessibleCamera.enablePointer();
            }
        }
        
    }
    
    void OnTriggerEnter(Collider col)
        {
        if (gameObject.tag == "openDiary")
        {

            Dialog_Panel.SetActive(true);

            UAP_AccessibilityManager.Say("Opened diary ");
                
            Text[] textObj;
            textObj = GameObject.Find("Dialog_Panel").GetComponentsInChildren<Text>();

            if (GameObject.Find("Dialog_Panel/Text").GetComponent<Text>())
            {
                UAP_AccessibilityManager.Say(textObj[0].text, true);
                UAP_AccessibilityManager.Say(textObj[1].text, true);
            }

            accessibleCamera.disablePointer();
            fpsController.GetComponent<FirstPersonController>().enabled = false;
        }

        if (gameObject.tag == "professor")
        {
            Dialog_Panel.SetActive(true);
            bumpcount = bumpcount + 1;
            fpsController.GetComponent<FirstPersonController>().enabled = false;
            
            Text[] textObj;
            textObj = GameObject.Find("Dialog_Panel").GetComponentsInChildren<Text>();

            if (bumpcount == 1)
            {
                UAP_AccessibilityManager.Say("You found the Professor. ");
                UAP_AccessibilityManager.Say(textObj[0].text, true);

            }
            else if (bumpcount > 1)
            {
                UAP_AccessibilityManager.Say(textObj[1].text, true);
            }

        }
    }

        void OnTriggerExit(Collider other)
        {
            Dialog_Panel.SetActive(false);
            if (UAP_AccessibilityManager.IsSpeaking() == false)
            {
                UAP_AccessibilityManager.Say("Closed dialog");
            }
            fpsController.GetComponent<FirstPersonController>().enabled = true;
            accessibleCamera.enablePointer();

        }


}