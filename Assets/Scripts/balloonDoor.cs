using UnityEngine;
using System.Collections;


public class balloonDoor : MonoBehaviour
{
    public GameObject rotateDoor;
    public GameObject DoorOpen;
    public AudioSource[] sounds;
    public AudioSource hit;
    public AudioSource opendoor;
    RotateDoor rotateDoorScript;


    void Start()
    {
        rotateDoor = GameObject.FindGameObjectWithTag("balloonDoor");
        rotateDoorScript = rotateDoor.GetComponent<RotateDoor>();
        DoorOpen = GameObject.FindWithTag("balloonDoorOpen");
        DoorOpen.SetActive(false);
        sounds = rotateDoor.GetComponents<AudioSource>();
        hit = sounds[0];
        opendoor= sounds[1];
    }

    public void openBalloonDoor()
    {
        StartCoroutine(PlayOpeningDoor());
        PlayerPrefs.SetInt("Balloondoor", 1);
    }

    IEnumerator PlayOpeningDoor()
    {
        opendoor.Play();
        rotateDoorScript.OpenDoor();

        StartCoroutine(ShowDoor());
        yield return new WaitForSeconds(3.0f);
    }
    IEnumerator ShowDoor()
    {
        DoorOpen.SetActive(true);
        yield return new WaitForSeconds(3.2f);
        

    }
}