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

    Vector3 currentPoint;

    public float force = 20f;
    float forceRate = 10f;
    float forceMin = 20f;
    float forceMax = 40f;
    public float angle;
    float angleRate = 1f;
    Vector3 angleApplied = Vector3.forward;

    static bool turnStarted;
    static bool inPrep;
    static bool isLaunched;
    static bool isInstant;

    private void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager> ();
        Debug.Log(lm.ToString());
    }
    void Update()
    {
        if (inPrep)
        {
            AimMovement();
        }

        if (!turnStarted)
        {
            // For each turn
            if (lm.turn <= lm.turnMax)
            {
                // Init the turn
                StartTurn(lm.turn);
            }
            // After all the turns
            else if (lm.turn == lm.turnMax + 1)
            {
                // Scores
                //scores();
                //TODO Check? Remove all players objects
                //DestryAllPlayers();
            }
        }
        else 
        {
            // controller
            Play();
        }

        
    }

    void AimMovement()
    {
        // Angle Swing
        angle = angle + (angleRate * Time.deltaTime);

        // Set angle as a Vector3
        angleApplied = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

        Debug.DrawRay(rb.transform.position, angleApplied, Color.green);

        // Angle Bounds
        if (angle <= 0.45f || angle >= 2.7f)
        {
            angleRate = -angleRate;
        }
        


        // Take Input
        float horizontal = Input.GetAxis("Horizontal");

        // Set Bounds
        if (transform.position.x <= -4.5f || rb.transform.position.x <= -4.5f)
        {
            transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
            rb.transform.position = new Vector3(-4.5f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 4.5f || rb.transform.position.x >= 4.5f)
        {
            transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
            rb.transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
        }

        // Move
        transform.Translate (Vector3.right * horizontal * 3 * Time.deltaTime);
        rb.transform.Translate(Vector3.right * horizontal * 3 * Time.deltaTime);

        // When space is held, charge force amount
        if (Input.GetKey(KeyCode.Space))
        {
            angleRate = 0;

            // Charge
            force = force + (forceRate * Time.deltaTime);

            // Flux other direction
            if (force >= forceMax || force <= forceMin)
            {
                forceRate = -forceRate;
            }
            

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

            player = Instantiate(lm.prefabPuck[num], transform.position, transform.rotation) as GameObject;
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
            if (rb.linearVelocity.magnitude < 0.1)
            {
                ChangeTurn();
                Debug.Log("Is Not Moving");
            }
        }
            
    }

    void Launch()
    {
        isLaunched = true;

        // Need to add angle

        //Apply Force
        Debug.Log("Launched");
        player.GetComponent<Rigidbody>().AddForce (angleApplied * force, ForceMode.Impulse);
    }

    void ChangeTurn()
    {
        // Reset variables
        force = forceMin;
        angleApplied = Vector3.forward;
        angleRate = 1f;
        lm.turn += 1;
        isInstant = false;
        turnStarted = false;
    }
}
