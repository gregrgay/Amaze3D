using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadMenu : MonoBehaviour
{
    public GameObject exitControl;
    public GameObject fpsController;
    public UI_Inventory uiInventory;
    public Inventory inventory;
    public Button exitGameButton;
    

    private void Start()
    {
        exitControl.SetActive(false);
        exitGameButton.Select();      

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            exitControl.SetActive(true);
            fpsController.GetComponent<FirstPersonController>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitControl.SetActive(false);
            fpsController.GetComponent<FirstPersonController>().enabled = true;
        }

    }
    public void ReturnToMenu()
    {
        fpsController.GetComponent<FirstPersonController>().enabled = true;
        SceneManager.LoadScene("Game Menu");
    }

    public void CancelExit()
    {
        exitControl.SetActive(false);
        fpsController.GetComponent<FirstPersonController>().enabled = true;
    }

}
