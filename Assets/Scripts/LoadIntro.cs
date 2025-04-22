using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadIntro : MonoBehaviour
{
    public UI_Inventory uiInventory;
    Inventory inventory; 
    public void LoadA()
    {

        GameObject obj = GameObject.Find("Inventory");

        if (obj)
        {
            Destroy(obj);
            Debug.Log("destroyed inventory");

        }
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
        PlayerPrefs.DeleteKey("CrackedWall (3)");
        PlayerPrefs.DeleteKey("Lever");
        ResetInventory();

        SceneManager.LoadScene("Intro");
    }

    private void ResetInventory()
    {

        if (inventory != null)
        {
            inventory = null;
            Debug.Log("destroyed inventory2");
        }
    }

}