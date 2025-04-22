using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#pragma warning disable 618, 649
//namespace UnityStandardAssets.Characters.FirstPerson
//{
[RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]

[System.Serializable]
public class FirstPersonController : MonoBehaviour
    {
        //Default game objects for FPS
        [SerializeField] private bool m_UseStrafe;
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook2 m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        //Added Game Objects for FPS
       [SerializeField]
        public static Inventory inventory;
        public UI_Inventory uiInventory;
        public GameObject metalPlate;
        public GameObject metalPlatesm;
        public GameObject doorClosed;
        public UnityEngine.UI.Button button1;
        public UnityEngine.UI.Button button2;
        public UnityEngine.UI.Button button3;
        public UnityEngine.UI.Button button4;
        public LoadLevel1 loadLevel1;
        public LoadLevel2 loadLevel2;
        public LoadLevel3 loadLevel3;
        public LoadLevel4 loadLevel4;
        public LoadLevel5 loadLevel5;
        public chestController chestController;
        public cwallController cwallController;
        public Lever lever;
        public GameObject frozenlever;
        public RemoteDoorController remotedoor;
        public balloonDoor balloonDoor;
        public BalloonController balloon;
        public GameObject catPanel;
        public GameObject catPaneltext;
        public GameObject puzzle1Canvas;
        public GameObject puzzle2Canvas;
        public int bumpprof;
        public int[] theseItems = new int[5];
        public SliderValue sliderValue;
        public Lock2Controller lock2Controller;
        public string scene;


    //Default variables
    private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
        PositionPlayer playerPosData;
        private int LastRotAngle = -999;
        private string DirectionFacing = "";
        private string LastDirectionFacing = "";

    // Loads saved game when Resume Saved button is clicked
    private void Awake()
    {
        playerPosData = FindObjectOfType<PositionPlayer>();
        playerPosData.PlayerPos();
    }
    // Use this for initialization
    private void Start()
        {

            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            scene = sceneName;

        
            if (inventory == null || (sceneName == "Level 1" && !PlayerPrefs.HasKey("p_x")))
            {
                inventory = new Inventory();
            }

    
            uiInventory = GameObject.FindGameObjectWithTag("ui_inventory").GetComponent<UI_Inventory>();
            uiInventory.SetInventory(inventory);

            if (sceneName == "Level3")
            {
                if (PlayerPrefs.GetInt("Lever") != 1)
                {
                frozenlever = GameObject.FindWithTag("frozen");
                frozenlever.SetActive(false);
                }
                
            }

            if (sceneName == "Level1")
            {
                puzzle1Canvas.SetActive(false);
                catPanel = GameObject.FindGameObjectWithTag("cat_panel");
                catPanel.SetActive(false);
                catPaneltext = GameObject.FindGameObjectWithTag("cat_panel_text");
                catPaneltext.SetActive(false);
            }

            if (sceneName == "Level 2")
            {

                metalPlate.SetActive(false);
                metalPlatesm.SetActive(false);
                puzzle2Canvas.SetActive(false);
            }
        }
            // Update is called once per frame
    private void Update()
    {
        RotateView();
        // the jump state needs to read here to make sure it is not missed
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            StartCoroutine(m_JumpBob.DoBobCycle());
            PlayLandingSound();
            m_MoveDir.y = 0f;
            m_Jumping = false;
        }
        if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
        {
            m_MoveDir.y = 0f;
        }

        m_PreviouslyGrounded = m_CharacterController.isGrounded;
        

    }

    private bool checkRange(int num, int compareTo, int range) {
        //Debug.Log("checing to see if "+num+" is within "+range+" of "+compareTo);
        if (num >= (compareTo - range) && num <= (compareTo+ range)) {
            return true;
        } else {
            return false;
        }
    }

    private void announceFacing()
    {
        //Debug.Log("New angle");
        var yAngle = (int)transform.eulerAngles.y;
        if (checkRange(yAngle,0,4) || checkRange(yAngle,360,4)) {
            //Debug.Log("North");
            DirectionFacing = "North";
        } else if (checkRange(yAngle,90,4)) {
            //Debug.Log("East");
            DirectionFacing = "East";
        } else if (checkRange(yAngle,180,4)) {
            //Debug.Log("South");
            DirectionFacing = "South";
        } else if (checkRange(yAngle,270,4)) {
            //Debug.Log("West");
            DirectionFacing = "West";
        } else {
            DirectionFacing = "";
        }

        if (DirectionFacing != LastDirectionFacing) {
            if (DirectionFacing != "") {
                UAP_AccessibilityManager.Say("Facing "+DirectionFacing);
            }
            LastDirectionFacing = DirectionFacing;
        }

    }

    //private void InitializeCustomTTS()
    //{
    //    UAP_CustomTTS.InitializeCustomTTS<Custom_Amaze_TTS>();
    //    Debug.Log("State is: " +UAP_CustomTTS.TTSInitializationState.Initialized);
    //    //AmazeTTS::SpeakText(" hey its working, or not", 300);
    //    Debug.Log(UAP_CustomTTS.TTSInitializationState.Initialized);

    //}

        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y;// + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
            
            
            Quaternion existingRot = transform.rotation;

            //Debug.Log("Existing rotation is "+existingRot.eulerAngles);

            float newRotY = existingRot.eulerAngles.y+m_Input.normalized.x;
            //Debug.Log("Input: "+m_Input.x+" Normalized: "+m_Input.normalized.x);
            Quaternion newRot = Quaternion.Euler(existingRot.eulerAngles.x, newRotY, existingRot.eulerAngles.z);

            transform.localRotation = newRot;

            //Debug.Log("Controller:"+m_Input.x+" applied to rotation:"+existingRot.y+" to get "+newRotY+" and "+newRot);
            


            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();

            announceFacing();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;

            //Debug.Log("Camera is "+m_Input.x);

            //m_Camera.transform.rotation.y += m_Input.x;

            //m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);


            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            //Debug.Log("Raw input h "+horizontal+" v "+vertical);

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }

    public void SnapRotationToLook(GameObject other)
    {
        transform.rotation = Quaternion.LookRotation (other.transform.position - transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    private void OnTriggerEnter(Collider other)
        {
            // If First Person bumps into a collectable, add it to the inventory
            switch (other.gameObject.tag)
            {
                default:
                case "locked chest": break;
                case "bluegem": inventory.AddItem(new Item { itemType = Item.ItemType.bluegem, amount = 1 }); break;
                case "yellowgem": inventory.AddItem(new Item { itemType = Item.ItemType.yellowgem, amount = 1 }); break;
                case "pinkgem": inventory.AddItem(new Item { itemType = Item.ItemType.pinkgem, amount = 1 }); break;
                case "key": inventory.AddItem(new Item { itemType = Item.ItemType.key, amount = 1 }); break;
                case "cats_photo": inventory.AddItem(new Item { itemType = Item.ItemType.cat_small, amount = 1 }); break;
                case "panel_small": inventory.AddItem(new Item { itemType = Item.ItemType.panel_small, amount = 1 }); break;
                case "nitrogen_spray": inventory.AddItem(new Item { itemType = Item.ItemType.nitrogen, amount = 1 }); break;
            }

            var inventoryItems = inventory.GetItemList();
        
            if (other.gameObject.tag == "locked chest")
            {

                Scene currentScene = SceneManager.GetActiveScene();
                string sceneName = currentScene.name;

                if (inventory.CheckItem(new Item { itemType = Item.ItemType.key, amount = 1 }))
                {
                    //
                    chestController = other.GetComponent<chestController>();
                    chestController.openChest();
                    inventory.RemoveItem(new Item { itemType = Item.ItemType.key, amount = 1 });
                    uiInventory.RefreshInventoryItems();
                }
                else
                {
                //if (PlayerPrefs.GetInt("nitrogen_spray") != 1
                //    && PlayerPrefs.GetInt("panel_small") != 1
                //    && PlayerPrefs.GetInt("cat_photo") != 1)
                //{
                //    UAP_AccessibilityManager.Say("You need a key to open the chest");
                //}
                Debug.Log(sceneName);
                    switch (sceneName)
                    {
                        default:
                        case "Level 1":
                            if (PlayerPrefs.GetInt("cats_photo") != 1)
                            {
                                UAP_AccessibilityManager.Say("You need a key to open the chest");
                            }
                            break;
                        case "Level 2":
                            if (PlayerPrefs.GetInt("panel_small") != 1)
                            {
                            Debug.Log("speaking");
                            UAP_AccessibilityManager.Say("You need a key to open the chest");
                            Debug.Log("not speaking");
                            }
                            break;
                        case "Level3": 
                            if (PlayerPrefs.GetInt("nitrogen_spray") != 1)
                            {
                                UAP_AccessibilityManager.Say("You need a key to open the chest");
                            }
                            break; 
                    }
                }
            }
            if (other.gameObject.tag == "cracked_wall")
            {
                cwallController = other.GetComponent<cwallController>();
                cwallController.bumpWall();
                //uiInventory.RefreshInventoryItems();
            }

            if (other.gameObject.tag == "Lever")
            {

                AudioSource dooropening = other.GetComponent<AudioSource>();
                if (PlayerPrefs.GetInt("Lever") == 1)
                {
                    remotedoor.OpenRemote();
                    frozenlever.SetActive(true);
                }
                else
                { 
                    if (lever.GetLeverState() == true)
                    {
                        remotedoor.OpenRemote();
                    
                        int resumePrefs = PlayerPrefs.GetInt("Lever");
                        if (resumePrefs == 1)
                        {
                            frozenlever.SetActive(true);
                            remotedoor.OpenRemote();
                        }
                        else if (inventory.CheckItem(new Item { itemType = Item.ItemType.nitrogen, amount = 1 }))
                        {
                            //good to go
                            frozenlever.SetActive(true);
                            dooropening.Play();
                            PlayerPrefs.SetInt("Lever", 1);
                            PlayerPrefs.DeleteKey("nitrogen_spray");
                            UAP_AccessibilityManager.Say("Lever pushed and frozen. Remote door has opened.");
                            inventory.RemoveItem(new Item { itemType = Item.ItemType.nitrogen, amount = 1 });
                            uiInventory.RefreshInventoryItems();
                        }
                        else if (frozenlever.activeInHierarchy == false)
                        {
                            //need to collect nitrogen first, so reset the lever
                            dooropening.Play();
                            UAP_AccessibilityManager.Say("Lever pushed. Remote door has opened.");
                            StartCoroutine(PauseLever());
                        }
                    }
                    else if (lever.GetLeverState() == false && frozenlever.activeInHierarchy == false)
                    {
                        remotedoor.CloseRemote();

                    }

                    IEnumerator PauseLever()
                    {
                        lever = other.GetComponent<Lever>();
                        //Delay before lever resets, when freeze spray not present.
                        yield return new WaitForSeconds(15);
                        dooropening.Play();
                        UAP_AccessibilityManager.Say("Lever reset. Remote door has closed.");
                        remotedoor.CloseRemote();
                        lever.ToggleLever();
                    }
                }
            }
            if (other.gameObject.tag == "balloonDoor")
            {
                balloonDoor = other.GetComponent<balloonDoor>();

                if (inventory.CheckItem(new Item { itemType = Item.ItemType.key, amount = 1 }))
                {

                    balloonDoor.openBalloonDoor();
                    PlayerPrefs.GetInt("Balloondoor", 1);
                    UAP_AccessibilityManager.Say("Door opened.");
                    inventory.RemoveItem(new Item { itemType = Item.ItemType.key, amount = 1 });
                    uiInventory.RefreshInventoryItems();
                }
                else if (PlayerPrefs.GetInt("Balloondoor") != 1)
                {
                    UAP_AccessibilityManager.Say("You need a key to open this door");
                }
            }
            if (other.gameObject.tag == "button_pad")
            {
                puzzle1Canvas.SetActive(true);

                if (inventory.CheckItem(new Item { itemType = Item.ItemType.cat_small, amount = 1 }))
                {    
                    catPanel.SetActive(true);

                    if (catPanel.active == true)
                    {
                        UAP_AccessibilityManager.Say("Use arrow keys to navigate. Use Enter key to activate. Escape key to exit the puzzle.");
                        
                    }
                }
                else
                {
                      UAP_AccessibilityManager.Say("You need the cat's photo for the combination to this lock.");

                        
                }
            }
            if (other.gameObject.tag == "button_pad2")
            {
                puzzle2Canvas.SetActive(true);

                if (inventory.CheckItem(new Item { itemType = Item.ItemType.panel_small, amount = 1 }) && PlayerPrefs.GetInt("door_closed") !=1)
                {

                    metalPlate.SetActive(true);
                    Button[] buttons = puzzle2Canvas.GetComponentsInChildren<Button>(true);
                    
                    if (metalPlate.active == true && PlayerPrefs.GetInt("door_closed") !=  1)
                    {
                        UAP_AccessibilityManager.Say("Use arrow keys to navigate. Use Enter key to activate. Escape key to exit the puzzle.");
                        lock2Controller.SetButtonLetters();
                        lock2Controller.EnableButtons();
                    }

                }
                else
                {

                    if (metalPlate.active == false)
                    {
                        UAP_AccessibilityManager.Say("You need the metal plate to open this lock.");

                }

                }
            }
            if (other.gameObject.tag == "balloon")
            {
                balloon = other.GetComponent<BalloonController>();
                
            }
            
        }


        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "exit")
            {
                if (loadLevel2 = other.GetComponent<LoadLevel2>())
                {
                    loadLevel2.LoadA("Level 2");
                }
                else
                {
                    Debug.Log("Could not load Level 2");
                }

            } else if (other.gameObject.tag == "exit2")
            {
                if (loadLevel3 = other.GetComponent<LoadLevel3>())
                {
                    loadLevel3.LoadA("Level3");

                }
                else
                {
                    Debug.Log("Could not load Level 3");
                }

            }
            else if (other.gameObject.tag == "exit3")
            {
                if (loadLevel4 = other.GetComponent<LoadLevel4>())
                {
                    loadLevel4.LoadA("Level4");
                }
                else
                {
                    Debug.Log("Could not load Level 4");
                }

            }
            else if (other.gameObject.tag == "exit4")
            {
                if (loadLevel5 = other.GetComponent<LoadLevel5>())
                {
                    loadLevel5.LoadA("Level5");
                }
                else
                {
                    Debug.Log("Could not load Level 5");
                }

                
            }
            else if (other.gameObject.tag == "professor")
            {
                bumpprof = bumpprof + 1;

                if (bumpprof == 1) {
                    int times = 4;
                    for (int i = 0; i < times; i++)
                    {
                        //play ka-ching sound a few times when collecting the prof's booty
                        other.GetComponent<AudioSource>().Play();
                    }

                    inventory.AddItem(new Item { itemType = Item.ItemType.bluegem, amount = 3 });
                    inventory.AddItem(new Item { itemType = Item.ItemType.yellowgem, amount = 3 });
                    inventory.AddItem(new Item { itemType = Item.ItemType.pinkgem, amount = 3 });
                    uiInventory.RefreshInventoryItems();
                    UAP_AccessibilityManager.Say("The Professor's gems were added to yours.");
                    bumpprof = bumpprof + 1;

                } else if (bumpprof > 1) {
                    //don't do anything
                }

            } else if (other.gameObject.tag == "button_pad")
            {
                puzzle1Canvas.SetActive(false);

            }

        }

    }

//}