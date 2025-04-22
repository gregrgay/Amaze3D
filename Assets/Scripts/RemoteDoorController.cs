using UnityEngine;

public class RemoteDoorController : MonoBehaviour
{
    public GameObject exit1;

    void Start()
    {
        exit1 = GameObject.FindWithTag("closedexit");

    }

    public void OpenRemote()
    {
        exit1.SetActive(false);

    }

    public void CloseRemote()
    {
        exit1.SetActive(true);

    }
}