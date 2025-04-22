using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevel1 : MonoBehaviour
{

    public void LoadA()
    {
        PlayerPrefs.DeleteKey("LevelSaved");
        PlayerPrefs.DeleteKey("key");
        PlayerPrefs.DeleteKey("bluegem");
        PlayerPrefs.DeleteKey("pinkgem");
        PlayerPrefs.DeleteKey("yellowgem");
        PlayerPrefs.DeleteKey("cats_photo");
        PlayerPrefs.DeleteKey("chest_closed");
        PlayerPrefs.DeleteKey("door_closed");
        PlayerPrefs.DeleteKey("panel_small");
        SceneManager.LoadScene("Level 1");

    }
}