using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    LevelManager lm;
    AfterLaunchAbility ala;
    LaunchAbility la;

    public static int tunaPoints; 

    public Canvas arrowVisual;
    public Transform arrowAngle;
    public RectTransform arrowScale;

    GameObject player;
    Rigidbody rb;

    

    float force = 20f;
    float forceRate = 10f;
    float forceMin = 20f;
    float forceMax = 40f;

    float angle = 180f;
    float angleRate = 30f;

    public static bool playing = false;
    static bool turnStarted;
    public static bool inPrep;
    static bool isLaunched;
    static bool isInstant;

    // Stuff for the Intro Scene
    public static bool doneIntro;
    public static bool canMove = false;
    public static bool canAim = false;
    public static bool canLaunch = false;

    public static bool hasLaunchAbility = false;
    public static bool hasAfterAbility = false;

    InputAction movementKeys;
    InputAction jumpKey;

    //hats system
    //public MeshFilter[] hatFilter;
    //public Mesh[] hatModels;
    public static int hatEquipped;
    public GameObject[] hats;
    public bool[] hatsUnlocked;

    private void Awake()
    {
        // Establish Inputs
        movementKeys = InputSystem.actions.FindAction("Move");
        jumpKey = InputSystem.actions.FindAction("Jump");            

        SceneManager.sceneLoaded += OnSceneLoaded;

        Debug.Log("hide all hats behind gacha MUAHAHAHAA");
        foreach (var hatUnlocked in hatsUnlocked) 
        {
            hatUnlocked.Equals(false);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Get Level manager from scene
        if (SceneManager.GetActiveScene().name != "GachaSceneBuild")
        {
        lm = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        }
    }

    void Update()
    {
        if (playing)
        {
            if (inPrep && canMove)
            {
                // Allow player to move and aim arrow swings
                AimMovement();
            }

            if (!turnStarted)
            {
                // For each turn
                if (lm.turn < lm.turnMax)
                {
                        // Init the turn
                        StartTurn(lm.turn);
                }
                // After all the turns
                else if (lm.turn == lm.turnMax)
                {
                    // Scores
                    lm.ScoreRound();

                    // Freeze for endScreen
                    playing = false;
                }
            }
            else
            {
                // Run Game Functinaliy
                Play();
            }
        }
    }

    void AimMovement()
    {
        if (canAim)
        {
            // Launch Angle visual movements
            AimingModel();
        }

        // Take Input
        float horizontal = movementKeys.ReadValue<Vector2>().x;

        // Set Input Bounds
        if (transform.position.x <= -lm.boardBounds || rb.transform.position.x <= -lm.boardBounds)
        {
            transform.position = new Vector3(-lm.boardBounds, transform.position.y, transform.position.z);
            rb.transform.position = new Vector3(-lm.boardBounds, rb.transform.position.y, rb.transform.position.z);
        }
        else if (transform.position.x >= lm.boardBounds || rb.transform.position.x >= lm.boardBounds)
        {
            transform.position = new Vector3(lm.boardBounds, transform.position.y, transform.position.z);
            rb.transform.position = new Vector3(lm.boardBounds, rb.transform.position.y, rb.transform.position.z);
        }

        // Move
        transform.Translate(Vector3.right * horizontal * 3 * Time.deltaTime);
        rb.transform.position = new Vector3(transform.position.x, 1.056f, -10.26124f);

        // When Space is Held, Charge Force Amount
        if (jumpKey.IsPressed() && canLaunch)
        {
            float charge = -100;
            // Stop Rotation
            angleRate = 0;

            // Charge
            force = force + (forceRate * 2 * Time.deltaTime);

            // Flux other direction
            if (force >= forceMax || force <= forceMin)
            {
                forceRate = -forceRate;
            }

            // Assign Value to charge visual
            charge = -100 + 5 * (force - 20);

            // Arrow Scales with Force
            arrowScale.anchoredPosition = new Vector3(0f, charge, 0f);

            Debug.DrawRay(rb.transform.position, rb.transform.up * (force / 10), Color.blue);
        }


    }

    void AimingModel()
    {
        // Turn on Arrow UI
        arrowVisual.enabled = true;

        // Angle for Model & Arrow
        angle = angle + (angleRate * Time.deltaTime);
        // Bound for that Angle
        if (angle <= 120f || angle >= 240f) angleRate = -angleRate;
        // Rotate Model and Arrow
        rb.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
        arrowAngle.rotation = Quaternion.Euler(180f, angle, 0);
        // Debug Ray
        Debug.DrawRay(rb.transform.position, rb.transform.up, Color.green);
    }

    public void StartTurn(int turn)
    {
        // Start Turn
        turnStarted = true;
        inPrep = true;

        // Spawn Puck
        SpawnPuck(turn);
    }
    void SpawnPuck(int num)
    {
        // If player is not created, creat it.
        if (!isInstant)
        {
            isLaunched = false;

            player = Instantiate(lm.prefabPlayerPuck[num], new Vector3(transform.position.x, transform.position.y + .75f, transform.position.z), Quaternion.Euler(new Vector3(-90f, 180f, 0f)));
            rb = player.GetComponent<Rigidbody>();
            ala = player.GetComponent<AfterLaunchAbility>();
            la = player.GetComponent<LaunchAbility>();

            //apply hats
            //hatFilter = player.GetComponentsInChildren<MeshFilter>();
            //hatFilter[1].mesh = hatModels[hatEquipped];

            Transform[] hatTransform = player.GetComponentsInChildren<Transform>();
            GameObject newHat = Instantiate(hats[hatEquipped], hatTransform[1]);
            newHat.GetComponent<MeshRenderer>().enabled = false;
            if (hatsUnlocked[hatEquipped])
            {
                newHat.GetComponent<MeshRenderer>().enabled = true;
            }

            Debug.Log("Puck spawned, name is:" + lm.prefabPlayerPuck[num].name);

            if (ala != null)
            {
                hasAfterAbility = true;
            }
            else if (la != null)
            {
                hasLaunchAbility = true;
            }

            isInstant = true;
        }
    }

    void Play()
    {
        // In aiming phase
        if (!isLaunched)
        {
            // When space is released, launch
            if (jumpKey.WasReleasedThisFrame() && canLaunch)
            {
                arrowVisual.enabled = false;
                inPrep = false;
                Launch();
                SoundEffects.instance.PlaySoundEffect(SoundEffects.instance.slide, transform, 1, null);
            }
        }
        else
        {
            // During launch ability
            if (jumpKey.WasReleasedThisFrame() && hasLaunchAbility) 
            {
                la.UseLaunchAbility(force);
            }

            // Detect when puck has stopped
            if (rb.linearVelocity.magnitude <= 0.01 && lm.VelocityZero())
            {
                // Check for ALA ability
                if(hasAfterAbility && !ala.hasTriggered)
                {
                    // Activate ALA ability
                    Debug.Log("Not moving, triggering additional actions");
                    ala.UseAbility();
                }
                else
                {
                    // No ALA ability, change turn
                    Debug.Log("Not moving and no additional actions left");
                    ChangeTurn();
                }
            }
        }

    }

    void Launch()
    {
        //Apply Force
        Debug.Log("Launched");
        rb.AddForce(rb.transform.up * force, ForceMode.Impulse);
        StartCoroutine(DelayCheck());
    }

    void ChangeTurn()
    {
        // Reset variables
        force = forceMin;
        angle = 180f;
        angleRate = 20f;

        //UI Set for arrow
        arrowScale.anchoredPosition = new Vector3(0f, -100f, 0f);

        // Add to turn count and prep for next turn
        lm.turn += 1;
        isInstant = false;
        turnStarted = false;
        hasAfterAbility = false;
        hasLaunchAbility = false;
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(0.1f);
        isLaunched = true;
        Debug.Log("Now checking for stop");
    }
}
