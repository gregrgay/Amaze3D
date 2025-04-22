using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOutro : MonoBehaviour
{

    public UI_Inventory uiInventory;
    public Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {


        GameObject obj = GameObject.Find("Inventory");

        if (obj)
        {
            Destroy(obj);

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

        SceneManager.LoadScene("Outro");
    }

}