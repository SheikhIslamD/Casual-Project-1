using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    
    LevelManager lm;

    GameObject player;
    Rigidbody rb;

    public float force = 20f;
    float forceRate = 10f;
    float forceMin = 20f;
    float forceMax = 40f;

    public float angle = 180f;    
    float angleRate = 20f;

    float launchAngle = 1.6f;
    float directionRate = -0.36f;
    Vector3 angleApplied = Vector3.forward;

    static bool playing = true;
    static bool turnStarted;
    static bool inPrep;
    static bool isLaunched;
    static bool isInstant;

    private void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager> ();
    }
    void Update()
    {
        if (playing)
        {


            if (inPrep)
            {
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

                    // Return Score Performance
                    if (lm.starTotal != 0)
                    {
                        Debug.Log("Congratulations! Total stars recieved is: " + lm.starTotal);
                    }
                    else
                    {
                        Debug.Log("Oh no! the Dog's won this time. Retry?");
                    }

                    playing = false;
                    //TODO Check? Remove all players objects
                    //DestryAllPlayers();
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
        //Visualization for model
        angle = angle + (angleRate * Time.deltaTime);
        // Angle Bounds
        if (angle <= 125f || angle >= 235f)
        {
            angleRate = -angleRate;
        }
        // Rotate Model
        rb.transform.rotation = Quaternion.Euler(-90f, angle, 0f);

        //Adjust launch Angle
        launchAngle = launchAngle + (directionRate * Time.deltaTime);
        // Angle Bounds
        if (launchAngle <= .6f || launchAngle >= 2.57f)
        {
            directionRate = -directionRate;
        }
        //Angle for launch set to Vector3
        angleApplied = new Vector3(Mathf.Cos(launchAngle), 0, Mathf.Sin(launchAngle));

        Debug.DrawRay(rb.transform.position, angleApplied, Color.green);

        

        

        // Take Input
        float horizontal = Input.GetAxis("Horizontal");

        // Set Bounds
        if (transform.position.x <= -6.5f || rb.transform.position.x <= -6.5f)
        {
            transform.position = new Vector3(-6.5f, transform.position.y, transform.position.z);
            rb.transform.position = new Vector3(-6.5f, rb.transform.position.y, rb.transform.position.z);
        }
        else if (transform.position.x >= 6.5f || rb.transform.position.x >= 6.5f)
        {
            transform.position = new Vector3(6.5f, transform.position.y, transform.position.z);
            rb.transform.position = new Vector3(6.5f, rb.transform.position.y, rb.transform.position.z);
        }

        // Move
        transform.Translate (Vector3.right * horizontal * 3 * Time.deltaTime);
        rb.transform.position = new Vector3(transform.position.x, 1.056f, -10.26124f);

        // When space is held, charge force amount
        if (Input.GetKey(KeyCode.Space))
        {
            // Stop Rotation
            angleRate = 0;
            directionRate = 0;

            // Charge
            force = force + (forceRate * Time.deltaTime);

            // Flux other direction
            if (force >= forceMax || force <= forceMin)
            {
                forceRate = -forceRate;
            }

            Debug.DrawRay(rb.transform.position, angleApplied * (force/10), Color.blue);
        }

        
    }
    void StartTurn(int turn)
    {
        // Start Turn
        turnStarted = true;
        inPrep = true;

        // Enable UI

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

            isInstant = true;
        }
    }

    void Play()
    {
        // In aiming phase
        if (!isLaunched)
        {
            // When space is released, launch
            if (Input.GetKeyUp(KeyCode.Space))
            {
                inPrep = false;
                Launch();
            }
        }
        else
        {
            
            // Detect when puck has stopped
            if (rb.linearVelocity.magnitude < 0.01)
            {
                ChangeTurn();
                Debug.Log("Is Not Moving");
            }
        }
            
    }

    void Launch()
    {
        // Need to add angle

        //Apply Force
        Debug.Log("Launched");
        player.GetComponent<Rigidbody>().AddForce (angleApplied * force, ForceMode.Impulse);
        StartCoroutine(DelayCheck());
        
    }

    void ChangeTurn()
    {
        // Reset variables
        force = forceMin;
        angle = 180f;
        launchAngle = 1.6f;
        angleApplied = Vector3.forward;
        angleRate = 20f;
        directionRate = -0.36f;
        lm.turn += 1;
        isInstant = false;
        turnStarted = false;
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(1f);
        isLaunched = true;
        Debug.Log("Now checking for stop");
    }
}
