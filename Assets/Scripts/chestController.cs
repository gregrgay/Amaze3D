using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chestController : MonoBehaviour
{
    private AudioSource playSound;
    public Inventory inventory;
    public List<Item> itemList;
    public GameObject chest_open;
    public GameObject cat_large;


    public void openChest()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 0.5f);
        chest_open.SetActive(true);
        cat_large.SetActive(true);
        PlayerPrefs.SetInt("chest_closed", 1);
        PlayerPrefs.Save();
    }


}
