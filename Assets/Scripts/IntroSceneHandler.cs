using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroSceneHandler : MonoBehaviour
{
    LevelManager lm;

    public GameObject[] Canvases;

    InputAction movementKeys;
    InputAction jumpKey;

    bool playOnce;
    bool shown;
    bool checking;
    bool checkingLaunch;

    private void Awake()
    {
        // Establish Inputs
        movementKeys = InputSystem.actions.FindAction("Move");
        jumpKey = InputSystem.actions.FindAction("Jump");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Get Level manager from scene
        lm = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();

        if (!PlayerController.doneIntro)
        {
            StartCoroutine(RunIntro());
        }
        else
        {
            lm.prefabEnemyPuck[0].SetActive(true);
        }
    }

    void Update()
    {
        if (checking)
        {
            // Take Input
            float horizontal = movementKeys.ReadValue<Vector2>().x;

            if (horizontal > 0 && !shown)
            {
                StartCoroutine(RunAimIntro());
            }

            if (lm.turn == lm.turnMax - 1 && !playOnce)
            {
                Canvases[6].SetActive(true);
                playOnce = true;
                PlayerController.doneIntro = true;
            }
        }
        if (checkingLaunch)
        {
            if (jumpKey.WasPressedThisFrame() || jumpKey.IsPressed())
            {
                Canvases[5].SetActive(false);
                Canvases[6].SetActive(false);
            }
        }
    }


    IEnumerator RunIntro()
    {
        yield return new WaitForSeconds(3f);
        Canvases[0].SetActive(false);
        Canvases[1].SetActive(true);

        yield return new WaitForSeconds(10f);
        Canvases[1].SetActive(false);
        Canvases[2].SetActive(true);

        lm.prefabEnemyPuck[0].SetActive(true);

        yield return new WaitForSeconds(8f);
        Canvases[2].SetActive(false);
        Canvases[3].SetActive(true);
        PlayerController.canMove = true;
        checking = true;
    }
    public IEnumerator RunAimIntro()
    {
        yield return new WaitForSeconds(1f);
        Canvases[3].SetActive(false);
        Canvases[4].SetActive(true);
        PlayerController.canAim = true;
        shown = true;

        yield return new WaitForSeconds(4f);
        Canvases[4].SetActive(false);
        Canvases[5].SetActive(true);
        PlayerController.canLaunch = true;
        checkingLaunch = true;
    }
}
