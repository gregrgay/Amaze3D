using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel4 : MonoBehaviour
{
    public void LoadA(string scenename)
    {


        //Reset PlayPrefs if they exist
        PlayerPrefs.DeleteKey("p_x");
        PlayerPrefs.DeleteKey("p_y");
        PlayerPrefs.DeleteKey("p_z");
        PlayerPrefs.DeleteKey("r_x");
        PlayerPrefs.DeleteKey("r_y");
        PlayerPrefs.DeleteKey("r_z");
        PlayerPrefs.DeleteKey("LevelSaved");
        PlayerPrefs.DeleteKey("key");
        PlayerPrefs.DeleteKey("bluegem");
        PlayerPrefs.DeleteKey("pinkgem");
        PlayerPrefs.DeleteKey("yellowgem");
        PlayerPrefs.DeleteKey("cats_photo");
        PlayerPrefs.DeleteKey("chest_closed");
        PlayerPrefs.DeleteKey("door_closed");
        PlayerPrefs.DeleteKey("panel_small");
        PlayerPrefs.DeleteKey("CrackedWall (1)");
        PlayerPrefs.DeleteKey("CrackedWall (2)");
        PlayerPrefs.DeleteKey("Lever");
        PlayerPrefs.DeleteKey("nitrogen_spray");

        SceneManager.LoadScene("Level4");
    }
}