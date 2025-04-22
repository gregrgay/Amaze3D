using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
   // public static Inventory inventory;
    public GameObject fpsController;
    public float p_x;
    public float p_y;
    public float p_z;
    public float r_x;
    public float r_y;
    public float r_z;

    public void SaveScene()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LevelSaved", activeScene);

        p_x = fpsController.transform.position.x;
        PlayerPrefs.SetFloat("p_x", p_x);

        p_y = fpsController.transform.position.y;
        PlayerPrefs.SetFloat("p_y", p_y);

        p_z = fpsController.transform.position.z;
        PlayerPrefs.SetFloat("p_z", p_z);

        r_x = fpsController.transform.eulerAngles.x;
        PlayerPrefs.SetFloat("r_x", r_x);

        r_y = fpsController.transform.eulerAngles.y;
        PlayerPrefs.SetFloat("r_y", r_y);

        r_z = fpsController.transform.eulerAngles.z;
        PlayerPrefs.SetFloat("r_z", r_z);

        PlayerPrefs.Save();
        Debug.Log("This scene:"+activeScene);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("LevelSaved"))
        {
            string levelToLoad = PlayerPrefs.GetString("LevelSaved");
            SceneManager.LoadScene(levelToLoad);

        }
    }
    public void PlayerPosLoad()
    {
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
    }
}
