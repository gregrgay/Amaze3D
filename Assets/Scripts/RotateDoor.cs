using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateDoor : MonoBehaviour
{

    Animator anim;
    public GameObject rotateDoor;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;

    }

    public void OpenDoor()
    { 
        anim.enabled = true;
        StartCoroutine(DoorDestroy());
    }

    IEnumerator DoorDestroy()
    {       
        yield return new WaitForSeconds(1.5f);
        Destroy(rotateDoor, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (PlayerPrefs.GetInt("door_closed") != 1 && sceneName != "Level4")
        {
            UAP_AccessibilityManager.Say("Use the botton pad to enter the combination that opens this door.");
        }


    }

}
