using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    public AudioSource playSound;
    
    void OnTriggerEnter(Collider other)
    {
        playSound.Play();
        UAP_AccessibilityManager.Say("You bumped " + gameObject.tag);       
    }
}
