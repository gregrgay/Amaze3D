using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessible3dObject : MonoBehaviour
{
    public GameObject customCollider;
    public GameObject customLabel3d;

    private GameObject obj;

    public string readoutName;

    public bool opaque = false;

    private Collider myCollider;
    private AccessibleLabel_3D label3d;
    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        if (customCollider) {
            myCollider = customCollider.GetComponent<Collider>();
            if (readoutName == null || readoutName == "") {
                readoutName = customCollider.name;
            }
        } else {
            myCollider = gameObject.GetComponent<Collider>();
            //Debug.Log("Readout name is "+readoutName);
            if (readoutName == null || readoutName == "") {
                readoutName = gameObject.name;
                //Debug.Log("Overriding with object name");
            }
        }

        if (customLabel3d) {
            label3d = customLabel3d.GetComponent<AccessibleLabel_3D>();
        } else {
            label3d = gameObject.GetComponent<AccessibleLabel_3D>();
        }
        if (label3d) {
            label3d.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetGameObject() {
        return obj;
    }

    public Collider GetCollider() {
        return myCollider;
    }

    public void DisableLabel()
    {
        if (label3d) {
            label3d.enabled = false;
        }
    }

    public void EnableLabel()
    {
        if (label3d) {
            label3d.enabled = true;
        }
    }

    public bool IsEnabled()
    {
        if (label3d) {
            return label3d.enabled;
        } else {
            return false;
        }
    }

    public Vector3 GetLocation()
    {
        return new Vector3(myCollider.bounds.center.x, myCollider.bounds.max.y, myCollider.bounds.center.z);
    }

    public Vector3 GetLocation(float y)
    {
        return new Vector3(myCollider.bounds.center.x, y, myCollider.bounds.center.z);
    }

    public string TestInViewport(Camera cam) {
        
        string returnVal = null;
        Vector3 boundsMinXMinZ = new Vector3(myCollider.bounds.min.x, cam.transform.position.y, myCollider.bounds.min.z);
        Vector3 boundsMinXMaxZ = new Vector3(myCollider.bounds.min.x, cam.transform.position.y, myCollider.bounds.max.z);
        Vector3 boundsMaxXMinZ = new Vector3(myCollider.bounds.max.x, cam.transform.position.y, myCollider.bounds.min.z);
        Vector3 boundsMaxXMaxZ = new Vector3(myCollider.bounds.min.x, cam.transform.position.y, myCollider.bounds.max.z);
        Vector3 boundsCenter = new Vector3(myCollider.bounds.center.x, cam.transform.position.y, myCollider.bounds.center.z);
        List<Vector3>boundsCollection = new List<Vector3>();
        boundsCollection.Add(boundsMaxXMaxZ);
        boundsCollection.Add(boundsMaxXMinZ);
        boundsCollection.Add(boundsMinXMaxZ);
        boundsCollection.Add(boundsMinXMinZ);
        boundsCollection.Add(boundsCenter);
        var inBounds = false;

        foreach (var bounds in boundsCollection)
        {
            Vector3 vport = cam.WorldToViewportPoint(bounds);
            if (vport.x < 1 && vport.x > 0 && vport.y < 1 && vport.y > 0 && vport.z > 0) {
                inBounds = true;
            }
        }

        if (inBounds) {
            Vector3 vportCenter = cam.WorldToViewportPoint(GetLocation(cam.transform.position.y));
            if (vportCenter.x < 0.4) {
                returnVal = "Left";
            } else if (vportCenter.x > 0.6) {
                returnVal = "Right";
            } else {
                returnVal = "Middle";
            }
        }
        
        return returnVal;
    }

}
