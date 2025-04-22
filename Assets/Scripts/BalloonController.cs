using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioSource boing;
    public AudioSource pop;
    public float force = 20;
    public int boingcount;
    public string thisBalloon;
    public GameObject thisBalloonObj;
    public bool holding;

    void Start()
    {
        //audioSource= GetComponent<AudioSource>();
    }


    void Update()
    {
        sounds = GetComponents<AudioSource>();
        pop = sounds[1];

        if (Input.GetMouseButtonDown(0)) {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                thisBalloon = hit.transform.gameObject.name.ToString();
                thisBalloonObj = hit.transform.gameObject;

                if (hit.transform != null)
                {
                    if(hit.transform.gameObject.name == thisBalloon && hit.transform.gameObject.tag == "balloon")
                    {
                        pop.Play();
                        thisBalloonObj.SetActive(false);
                        PlayerPrefs.SetInt(thisBalloon, 1);
                    }

                }
                
            }
           
        }
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            holding = true;

        }

        if (holding)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                    thisBalloon = hit.transform.gameObject.name.ToString();
                    thisBalloonObj = hit.transform.gameObject;

                    if (hit.transform != null)
                    {
                        if (hit.transform.gameObject.name == thisBalloon && hit.transform.gameObject.tag == "balloon")
                        {
                            pop.Play();
                            thisBalloonObj.SetActive(false);
                            PlayerPrefs.SetInt(thisBalloon, 1);
                            holding = false;
                        }

                    }

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        boingcount = boingcount+1;

        sounds = GetComponents<AudioSource>();
        boing = sounds[0];
        boing.Play();
        StartCoroutine(boingBalloon());
        if (boingcount == 3)
        {
            UAP_AccessibilityManager.Say("You need something pointy to pop balloons.");
        } else if(boingcount >= 5)
        {
            UAP_AccessibilityManager.Say("Try using your mouse.");
            UAP_AccessibilityManager.Say("or, Press Shift + Control to pop balloons in front of you.");
        }


    }
    public IEnumerator boingBalloon()
    {
        Vector3 originalLocalScale = transform.localScale;
        transform.localScale += new Vector3(-0.2F, -0.2f, -0.2f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalLocalScale;
    }
}

    
    