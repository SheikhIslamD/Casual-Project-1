using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    int turn = 0; // Should be in Level Manager
    int turnMax; // Same

    GameObject player;

    public GameObject[] prefabPuck; // Should Be in Level Manager
    Vector3 currentPoint;

    static bool turnStarted;
    static bool inPrep;
    static bool isCharged;
    static bool isLaunched;
    static bool isInstant;
    static bool isStopped;

    void Start()
    {
        // On Level Start, find array of pucks for level
            // LevelManager.AssignPucks()
                /* For length of puckCount[]
                   Assign puckPrefab to slot */
    }

    void Update()
    {
        if (inPrep)
        {
            AimMovement();
        }

        if (!turnStarted)
        {
            // For each rounds
            if (turn <= turnMax)
            {
                // Init the Turn
                StartTurn(turn);
                // Shuffle board
            }
            // After all the turns
            else if (turn == turnMax + 1)
            {
                // Scores
                //scores();
                //TODO Check? Remove all players objects
                //DestryAllPlayers();
            }
        }

        Play();
    }

    void AimMovement()
    { 
        // Move Left and Right in Fixed parameters
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate (Vector3.right * horizontal * 3 * Time.deltaTime);

        // Set Bounds


        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }

        
    }
    void StartTurn(int turn)
    {
        // Start Turn
        turnStarted = true;

        // Enable UI

        // Spawn Puck
        SpawnPuck(turn);

        
    }
    void SpawnPuck(int num)
    {
        // If player is not created, creat it.
        if (!isInstant)
        {
            isStopped = false;
            isLaunched = false;

            player = Instantiate(prefabPuck[num], transform.position, transform.rotation) as GameObject;

            isInstant = true;
        }
    }

    void Play()
    {
        if (!isLaunched)
        {
            // Enable Player Aim Capabilites
            StartCoroutine(PrepLaunch());
        }
        else
        {
            // Detect when puck has stopped
            if (player.GetComponent<Rigidbody>().linearVelocity.magnitude < 0.1)
            {
                ChangeTurn();
                Debug.Log("Is Not moving");
            }
        }
            
    }
    IEnumerator PrepLaunch()
    {
        while (!isLaunched)
        {
            inPrep = true;

            if (Input.GetKeyUp(KeyCode.Space))
            {

                Launch();
                yield return null;
            }
        }
    }

    void Launch()
    {

    }

    void ChangeTurn()
    {

    }
}
