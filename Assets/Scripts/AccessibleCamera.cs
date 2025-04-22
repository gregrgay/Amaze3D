using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessibleCamera : MonoBehaviour
{
    Accessible3dObject[] Label3d;
    string lastLabelRead;
    float countDown;
    public float maxCountDown = 5;

    private bool tempDisable = false;

    public float veryCloseThreshold = 10f;
    public float closeThreshold = 35f;

    public GameObject rotateable;

    Camera cam;
    LayerMask mask;

    int ignoreLayer;
    GameObject currentObject;

    List<Accessible3dObject> lastFocusedObjects = new List<Accessible3dObject>();
    int lastFocusedIndex = 0;
    Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        Label3d = Object.FindObjectsOfType<Accessible3dObject>(true);
        cam = this.GetComponent<Camera>();
        mask = LayerMask.GetMask("NavigationAides");
        ignoreLayer = LayerMask.NameToLayer("Accessible3dCameraIgnore");
    }

    void OnDrawGizmosSelected() 
    {
        if (Label3d != null) {
            foreach (var item in Label3d) {
                if (item != null) {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(item.transform.position, 1);
                }
            }
        }
    }

    public void disablePointer() {
        tempDisable = true;
    }

    public void enablePointer() {
        tempDisable = false;
    }

    int SortLeftToRight(Accessible3dObject p1, Accessible3dObject p2) {
        Debug.Log("Comparing p1 "+p1.name);
        Debug.Log("to "+p2.name);
        Vector3 dir1 = (p1.GetLocation() - cam.transform.position);
        Vector3 vportPt1 = cam.WorldToViewportPoint(p1.GetLocation());

        Vector3 dir2 = (p2.GetLocation() - cam.transform.position);
        Vector3 vportPt2 = cam.WorldToViewportPoint(p2.GetLocation());
        return vportPt1.x.CompareTo(vportPt2.x);
    }

    /*    void SetLayerOnAccessible3dObjects(Accessible3dObject target) {
        if (Label3d != null) {
            foreach (var item in Label3d) {
                item.gameObject.layer = 0;
                if (target != item && !item.opaque) {
                    item.gameObject.layer = transparentLayer;
                }
            }
        }
    }*/

    // Update is called once per frame
    void DescribeCurrentRoom() {
        List<AccessibleRoomDescription> rooms = new List<AccessibleRoomDescription>(Object.FindObjectsOfType<AccessibleRoomDescription>(true));
        AccessibleRoomDescription matchedRoom = null;
        foreach (var room in rooms) {
            if (room.testCollider.bounds.Contains(gameObject.transform.position)) {
                matchedRoom = room;
            }
        }
        if (matchedRoom != null) {
            UAP_AccessibilityManager.Say(matchedRoom.roomDescription);
        }
    }
    void AnnounceOnReader() {
        int described = 0;
        List<Accessible3dObject> Label3dTempPre = new List<Accessible3dObject>(Object.FindObjectsOfType<Accessible3dObject>(true));
        List<Accessible3dObject> Label3dTemp = new List<Accessible3dObject>();
        foreach (var item in Label3dTempPre) {
            if (item.isActiveAndEnabled) {
                Label3dTemp.Add(item);
            }
        }
        Label3dTemp.Sort(SortLeftToRight);
        Debug.Log("Sorted all the objects in order of the screen");
        foreach (var item in Label3dTemp) {
            if (item != null) {
                Debug.Log("***Testing hits against "+item.name);
                Vector3 dir = (item.GetLocation(cam.transform.position.y) - cam.transform.position);
                /*Vector3 vportPt = cam.WorldToViewportPoint(item.GetLocation(cam.transform.position.y));
                if (vportPt.x < 1 && vportPt.x > 0 && vportPt.y < 1 && vportPt.y > 0 && vportPt.z > 0) {
                    string xy = "";
                    if (vportPt.x < 0.4) {
                        xy = "Left";
                    } else if (vportPt.x > 0.6) {
                        xy = "Right";
                    } else {
                        xy = "Middle";
                    }*/
                var xy = item.TestInViewport(cam);
                if (xy != null ) {
                    RaycastHit[] hits;
                    hits = Physics.RaycastAll(cam.transform.position, dir, 999, ~mask, QueryTriggerInteraction.Collide);
                    /*if (item.name == "Open Door 1") {
                        Debug.DrawRay(cam.transform.position, dir, Color.red);
                    }*/
                    System.Array.Sort(hits, (x,y) => x.distance.CompareTo(y.distance));
                    var found = false;
                    foreach(var hit in hits) {
                        Debug.Log("Hit "+hit.collider.name);
                        if (!found) {
                            if (item.name == hit.collider.name) {
                                found = true;
                                Debug.Log("Found target item "+hit.collider.name);
                                if (item.name == hit.collider.name) {
                                    var distance = Vector3.Distance(hit.transform.position, transform.position);
                                    Debug.Log("Distance: " + distance);
                                    var distanceDesc = "";
                                    if (distance < veryCloseThreshold) {
                                        distanceDesc = "very close";
                                    } else if (distance < closeThreshold) {
                                        distanceDesc = "close by";
                                    } else {
                                        distanceDesc = "far away";
                                    }
                                // Debug.Log("Hit the actual label3 we're looking for "+item.name);
                                    UAP_AccessibilityManager.Say(item.readoutName +" in view "+distanceDesc+" to the "+xy);
                                    described++;
                                } else {
                                    //Debug.Log("Hit "+hit.collider.name+" instead");
                                }
                            } else if (hit.collider.gameObject.layer == ignoreLayer) {
                                Debug.Log("Found ignorable 3d object");
                            } else if (hit.collider.gameObject.GetComponent<AccessibleRoomDescription>() != null) {
                                Debug.Log("Found room description - can skip");
                            } else if (hit.collider.gameObject.GetComponent<Accessible3dObject>() == null) {
                                Debug.Log("Found non-3d object");
                                found = true;
                            } else if (hit.collider.gameObject.GetComponent<Accessible3dObject>().opaque) {
                                Debug.Log("Found an opaque 3d object");
                                found = true;
                            }
                        }
                    }
                } else {
                    Debug.Log("Object not in view");
                }
            }
        }
        if (described == 0) {
            UAP_AccessibilityManager.Say("No objects in view");
        }
    }
    void SnapRotationToLook(GameObject objectToLookAt)
    {
        rotateable.transform.rotation = Quaternion.LookRotation (objectToLookAt.transform.position - rotateable.transform.position);
        rotateable.transform.rotation = Quaternion.Euler(0, rotateable.transform.rotation.eulerAngles.y, 0);
    }
    void UpdateTabQueue() {
        List<Accessible3dObject> Label3dTempPre = new List<Accessible3dObject>(Object.FindObjectsOfType<Accessible3dObject>(true));
        List<Accessible3dObject> Label3dTemp = new List<Accessible3dObject>();
        foreach (var item in Label3dTempPre) {
            if (item.isActiveAndEnabled) {
                Label3dTemp.Add(item);
            }
        }
        Label3dTemp.Sort(SortLeftToRight);
        Debug.Log("Sorted all the objects in order of the screen");
        foreach (var item in Label3dTemp) {
            if (item != null) {
                Debug.Log("***Testing hits against "+item.name);
                Vector3 dir = (item.GetLocation(cam.transform.position.y) - cam.transform.position);
                /*Vector3 vportPt = cam.WorldToViewportPoint(item.GetLocation());
                if (vportPt.x < 1 && vportPt.x > 0 && vportPt.y < 1 && vportPt.y > 0 && vportPt.z > 0) {*/
                var xy = item.TestInViewport(cam);
                if (xy != null ) {
                    RaycastHit[] hits;
                    hits = Physics.RaycastAll(cam.transform.position, dir, 999, ~mask, QueryTriggerInteraction.Collide);
                    /*if (item.name == "Open Door 1") {
                        Debug.DrawRay(cam.transform.position, dir, Color.red);
                    }*/
                    System.Array.Sort(hits, (x,y) => x.distance.CompareTo(y.distance));
                    var found = false;
                    foreach(var hit in hits) {
                        Debug.Log("Hit "+hit.collider.name);
                        if (!found) {
                            if (item.name == hit.collider.name) {
                                found = true;
                                Debug.Log("Found target item "+hit.collider.name);
                                if (lastFocusedObjects.IndexOf(item) == -1) {
                                    Debug.Log(item.name+" is being added to tab order");
                                    lastFocusedObjects.Add(item);
                                }
                            } else if (hit.collider.gameObject.layer == ignoreLayer) {
                                Debug.Log("Found ignorable 3d object");
                            }
                            else if (hit.collider.gameObject.GetComponent<AccessibleRoomDescription>() != null) {
                                Debug.Log("Found room description - can skip");
                            } else if (hit.collider.gameObject.GetComponent<Accessible3dObject>() == null) {
                                Debug.Log("Found non-3d object");
                                found = true;
                            } else if (hit.collider.gameObject.GetComponent<Accessible3dObject>().opaque) {
                                Debug.Log("Found an opaque 3d object");
                                found = true;
                            }
                        }
                    }
                }
            }
        }
    }
    void Update()
    {
        if (!tempDisable) {
        
            if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0)) {
                if (lastFocusedObjects.Count > 0) {
                    Debug.Log("Reset tabs because of mouse movement");
                    lastFocusedObjects.Clear();
                    lastFocusedIndex = 0;
                }
            }
            if (Input.anyKeyDown) {
                if (!Input.GetKeyDown(KeyCode.Tab)) {
                    if (lastFocusedObjects.Count > 0) {
                        Debug.Log("Reset tabs because of key movement");
                        lastFocusedObjects.Clear();
                        lastFocusedIndex = 0;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {

                if (lastFocusedObjects.Count == 0) {
                    UpdateTabQueue();
                    lastFocusedIndex = 0;
                }

                Debug.Log("Tab between visible objects");

                Debug.Log(lastFocusedObjects.Count+" visible objects right now - presumably sorted in x order");

                if (lastFocusedObjects.Count == 0) {
                    Debug.Log("Nothing visible to tab between");
                    lastFocusedIndex = 0;
                } else {

                    

                    Debug.Log("Announcing item at index "+lastFocusedObjects);

                    Accessible3dObject Announceable = lastFocusedObjects[lastFocusedIndex];
                    Debug.Log("Announce item "+Announceable.name);
                    UAP_AccessibilityManager.StopSpeaking();
                    UAP_AccessibilityManager.Say("Turned to face "+Announceable.GetComponent<Accessible3dObject>().readoutName);
                    lastLabelRead = Announceable.name;
                    SnapRotationToLook(Announceable.gameObject);
                    var existingCount = lastFocusedObjects.Count;
                    UpdateTabQueue();
                    if (lastFocusedObjects.Count != existingCount) {
                        Debug.Log("After changing view, "+(lastFocusedObjects.Count - existingCount)+" new objects in view");
                    }

                    lastFocusedIndex++;

                    if (lastFocusedIndex >= lastFocusedObjects.Count) {
                        Debug.Log("Tabbed to the end - reverting to 0");
                        lastFocusedIndex = 0;
                    }
                }
            }

            if (countDown > 0) {
                countDown = countDown - Time.deltaTime;
                if (countDown <= 0) {
                    countDown = 0;
                    lastLabelRead = "";
                }
            }
            Ray ray2 =  Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));//Camera.main.ScreenPointToRay(Input.mousePosition);
        
            //RaycastHit hit2;
            if (UAP_AccessibilityManager.IsSpeaking() == false) {
                RaycastHit[] hits2;
                hits2 = Physics.RaycastAll(ray2, 999, ~mask, QueryTriggerInteraction.Collide);
                System.Array.Sort(hits2, (x,y) => x.distance.CompareTo(y.distance));
                //if (Physics.RaycastAll(ray2, 999, out hit2)) {
                var blocked = false;
                var message = "";
                foreach(var hit2 in hits2) {
                    if (!blocked) {
                        if (hit2.collider.gameObject.GetComponent<Accessible3dObject>() != null) {
                        
                            //Debug.Log("Found an item "+hit2.collider.name);
                            //currentObject = hit2.collider.gameObject;
                            
                            //UAP_AccessibilityManager.Say("Pointer is over " + currentObject.GetComponent<Accessible3dObject>().readoutName);
                            if (message == "") {
                                message = "Pointer is over "+hit2.collider.gameObject.GetComponent<Accessible3dObject>().readoutName;
                            } else {
                                if (!message.Contains(hit2.collider.gameObject.GetComponent<Accessible3dObject>().readoutName)) {
                                    message = message + " and " + hit2.collider.gameObject.GetComponent<Accessible3dObject>().readoutName;
                                }
                            }
                            if (hit2.collider.gameObject.GetComponent<Accessible3dObject>().opaque) {
                                blocked = true;
                            }
                        } else if (hit2.collider.gameObject.layer == ignoreLayer) {
                            //Debug.Log("Found ignorable 3d object");
                        }
                        else if (hit2.collider.gameObject.GetComponent<AccessibleRoomDescription>() != null) {
                            //Debug.Log("Found room description - can skip");
                        } else if (hit2.collider.gameObject.GetComponent<Accessible3dObject>() == null) {
                            //Debug.Log("Found non-3d object");
                            blocked = true;
                        }
                    }
                }
                
                if (message != lastLabelRead) {
                    UAP_AccessibilityManager.Say(message);
                    lastLabelRead = message;
                    countDown = maxCountDown;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.I)) {
                UAP_AccessibilityManager.Say("Describe View");
                DescribeCurrentRoom();
                lastFocusedObjects.Clear();
                lastFocusedIndex = 0;
                lastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                AnnounceOnReader();
            }
        }

    }
}
