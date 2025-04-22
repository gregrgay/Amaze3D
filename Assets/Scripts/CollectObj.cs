    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class CollectObj : MonoBehaviour
{

    private AudioSource playSound;
    public bool interactable;
    public string thisGameObject;
    public BoxCollider m_Collider;
    void Start()
    {
        interactable = true;
        m_Collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider otherObj)
    {
        if (interactable == true)
        {
            interactable = false;
            GetComponent<AudioSource>().Play();
            if (gameObject.tag == "panel_small")
            {
                UAP_AccessibilityManager.Say("You collected metal plate"); ;
            } else
            {
                UAP_AccessibilityManager.Say("You collected " + gameObject.tag);
            }

            PlayerPrefs.SetInt(gameObject.tag, 1);
            PlayerPrefs.Save();
            m_Collider.enabled = false;

            StartCoroutine(StopInteraction());
            Destroy(gameObject, 1.0f);

        }
    }
    IEnumerator StopInteraction()
    {
        yield return new WaitForSeconds(4.0f);
        interactable = true;
    }
}