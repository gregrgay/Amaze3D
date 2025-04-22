using UnityEngine;
using System.Collections;
public class cwallController : MonoBehaviour
{
    private AudioSource playSound;
    private int bumpCount;

    public void bumpWall()
    {
        string[] cwall_name = new string[3];

        GetComponent<AudioSource>().Play();

        if (bumpCount > 1)
        {
            UAP_AccessibilityManager.Say("You found a secret room.");
            PlayerPrefs.SetInt(gameObject.name, 1);
            Destroy(gameObject, 0.5f);
            bumpCount = 0;
        }
        else if (bumpCount == 0)
        {
            StartCoroutine(bumpCounter());
            UAP_AccessibilityManager.Say("The wall gave a little.");
        }
        else if (bumpCount == 1)
        {
            StartCoroutine(bumpCounter());
            UAP_AccessibilityManager.Say("The wall gave a little more.");
        }

    }
    IEnumerator bumpCounter()
    {
        bumpCount++;
        yield return new WaitForSeconds(1.0f);
        
    }
}
