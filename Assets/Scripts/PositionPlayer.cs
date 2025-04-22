using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionPlayer : MonoBehaviour
{
    public float p_x;
    public float p_y;
    public float p_z;
    public float r_x;
    public float r_y;
    public float r_z;

    public GameObject fpsController;
    public GameObject thisobj;
    public GameObject openChest;
    public GameObject exit1;
    public GameObject frozenlever;
    public GameObject opendoor;
    public GameObject thisBallon;
    public GameObject mapCam;
    public AudioSource bgmusic;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.M) && PlayerPrefs.GetInt("MapCam") == 1)
        {
            mapCam.SetActive(false);
            PlayerPrefs.DeleteKey("MapCam");
            PlayerPrefs.Save();
        }
        else if (Input.GetKeyUp(KeyCode.M) && PlayerPrefs.GetInt("MapCam") != 1)
        {
            mapCam.SetActive(true);
            PlayerPrefs.SetInt("MapCam", 1);
            PlayerPrefs.Save();
        }
        else if (PlayerPrefs.GetInt("MapCam") == 1)
        {
            mapCam.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Q) && PlayerPrefs.GetInt("Music") != 1)
        {
            bgmusic.Play();
            PlayerPrefs.SetInt("Music", 1);

        }
        else if (Input.GetKeyUp(KeyCode.Q) && PlayerPrefs.GetInt("Music") == 1)
        {
            bgmusic.Play();
            PlayerPrefs.DeleteKey("Music");

        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            bgmusic.Stop();
        }
    }
    public void PlayerPos()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == PlayerPrefs.GetString("LevelSaved"))
        {
            fpsController.GetComponent<CharacterController>().enabled = false;
            if (PlayerPrefs.HasKey("p_x"))
            {
                p_x = PlayerPrefs.GetFloat("p_x");
                p_y = PlayerPrefs.GetFloat("p_y");
                p_z = PlayerPrefs.GetFloat("p_z");
                r_x = PlayerPrefs.GetFloat("r_x");
                r_y = PlayerPrefs.GetFloat("r_y");
                r_z = PlayerPrefs.GetFloat("r_z");
                fpsController.transform.position = new Vector3(p_x, p_y, p_z);
                fpsController.transform.rotation = Quaternion.Euler(r_x, r_y, r_z);

                fpsController.GetComponent<CharacterController>().enabled = true;
            }
            if (PlayerPrefs.GetInt("bluegem") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("bluegem");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("yellowgem") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("yellowgem");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("pinkgem") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("pinkgem");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("key") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("key");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("cat_small") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("cat_small");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("chest_closed") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("locked chest");
                openChest.SetActive(true);
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("door_closed") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("door_closed");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("panel_small") == 1)
            {
                thisobj = GameObject.FindGameObjectWithTag("panel_small");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("CrackedWall (1)") == 1)
            {
                thisobj = GameObject.Find("CrackedWall (1)");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("CrackedWall (2)") == 1)
            {
                thisobj = GameObject.Find("CrackedWall (2)");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("CrackedWall (3)") == 1)
            {
                thisobj = GameObject.Find("CrackedWall (3)");
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("Lever") == 1)
            {
                thisobj = GameObject.Find("Exit");
                frozenlever = GameObject.FindGameObjectWithTag("frozen");
                frozenlever.SetActive(true);
                Destroy(thisobj);
            }
            if (PlayerPrefs.GetInt("Balloondoor") == 1)
            {
                thisobj = GameObject.Find("DoorClosed");
                Destroy(thisobj);
                opendoor = GameObject.Find("DoorOpen");
                opendoor.SetActive(true);
                for(int i=1; i<=18; i++)
                {
                    if (PlayerPrefs.GetInt("Balloon (" + i + ")") == 1)
                    {
                        thisBallon = GameObject.Find("Balloon (" + i + ")");
                        Destroy(thisBallon);
                    }
                }


            }

        }


    }
    public void PlayerPosLoad()
    {
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
    } 

}
