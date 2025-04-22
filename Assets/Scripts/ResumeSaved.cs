using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResumeSaved : MonoBehaviour
{
    public GameObject resumeSaved;
    public Button resumeButton;

    private void Start()
    {
        resumeButton = resumeSaved.GetComponent<Button>();
        resumeButton.interactable = false;
        if (PlayerPrefs.HasKey("LevelSaved"))
        {
            resumeButton.interactable = true;
        }
    }

}
