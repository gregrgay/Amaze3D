using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LessonDialog : MonoBehaviour
{

    public GameObject Lesson_Panel;
    public GameObject fpsController;
    public GameObject button;
    public LoadLevel2 loadLevel2;
    public LoadLevel3 loadLevel3;
    public LoadLevel4 loadLevel4;
    public LoadLevel5 loadLevel5;
    public LoadOutro loadOutro;
    public KeyCode _Key;



    private void Start()
    {
        Lesson_Panel.SetActive(false);
        loadLevel2 = Lesson_Panel.GetComponent<LoadLevel2>();
        loadLevel3 = Lesson_Panel.GetComponent<LoadLevel3>();
        loadLevel4 = Lesson_Panel.GetComponent<LoadLevel4>();
        loadLevel5 = Lesson_Panel.GetComponent<LoadLevel5>();
        fpsController = GameObject.Find("FPSController");
        fpsController.GetComponent<FirstPersonController>().enabled = true;

        
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
            
        if (Input.GetKeyDown(_Key))
        {
            switch (sceneName)
            {
                default:
                case "Level 1": loadLevel2.LoadA("Level 2"); break;
                case "Level 2": loadLevel3.LoadA("Level3"); break;
                case "Level3": loadLevel4.LoadA("Level4"); break;
                case "Level4": loadLevel5.LoadA("Level5"); break;
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        Text[] textObj;
        Lesson_Panel.SetActive(true);
        UAP_AccessibilityManager.Say("Opened Lesson ");
        fpsController.GetComponent<FirstPersonController>().enabled = false;
        
        textObj = GameObject.Find("Lesson_Panel").GetComponentsInChildren<Text>();

        if (GameObject.Find("Lesson_Panel/Text").GetComponent<Text>())
        {
            UAP_AccessibilityManager.Say(textObj[0].text, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Lesson_Panel.SetActive(false);
        UAP_AccessibilityManager.Say("Closed lesson.");
        fpsController.GetComponent<FirstPersonController>().enabled = true;

    }
}