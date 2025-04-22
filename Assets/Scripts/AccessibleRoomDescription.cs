using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessibleRoomDescription : MonoBehaviour
{
    [System.NonSerialized] public string roomName;
    public string roomDescription = "";
    [System.NonSerialized] public Collider testCollider;
    // Start is called before the first frame update
    void Start()
    {
        roomName = gameObject.name;
        testCollider = gameObject.GetComponent<Collider>();
        
    }

    public void UpdateRoomDescription() 
    {
        string newString = "Room "+roomName+". ";
        List<Accessible3dObject> contains = new List<Accessible3dObject>();
        Accessible3dObject[] Label3d = Object.FindObjectsOfType<Accessible3dObject>(true);
        foreach (var item in Label3d) {
            if (item != null) {
                Debug.Log("testing against "+item.readoutName);
                if (testCollider && item.GetCollider()) {
                    if (testCollider.bounds.Intersects(item.GetCollider().bounds)) {
                        contains.Add(item);
                    }
                }
            }
        }
        if (contains.Count == 0) {
            newString = newString + " contains nothing";
        } else {
            newString = newString + " contains ";
            foreach (var containing in contains) {
                newString = newString + containing.readoutName + ". ";
            }
        }
        roomDescription = newString;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roomDescription == "") {
            UpdateRoomDescription();
            Debug.Log(roomDescription);
        }
    }
}
