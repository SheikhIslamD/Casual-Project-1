using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroSceneHandler : MonoBehaviour
{
    LevelManager lm;

    public GameObject[] Canvases;

    InputAction movementKeys;
    InputAction jumpKey;

    int activeCanvas = 0;
    bool delay;


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
            PlayerController.playing = false;
            Canvases[0].SetActive(true);
            StartCoroutine(RunIntro());
        }
        else
        {
            lm.prefabEnemyPuck[0].SetActive(true);
        }
    }

    void Update()
    {
        
    }

    IEnumerator RunIntro()
    {
        yield return new WaitUntil(jumpKey.WasReleasedThisFrame);
        canvasSwap(activeCanvas, activeCanvas + 1);

        if (activeCanvas == 6)
        {
            Canvases[6].SetActive(false);
            yield break;
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RunIntro());
    }

    IEnumerator RunSpecial()
    {
        Canvases[6].SetActive(true);
        yield return new WaitUntil(jumpKey.WasReleasedThisFrame);
        Canvases[6].SetActive(false);
    }

    // When interacted with skip canvas
    void canvasSwap(int current, int next)
    {
        Canvases[current].SetActive(false);
        Canvases[next].SetActive(true);
        activeCanvas++;

        switch (activeCanvas)
        {
            case 1:
                PlayerController.playing = true;
                break;
            case 2:
                lm.prefabEnemyPuck[0].SetActive(true);
                break;
            case 3:
                PlayerController.canMove = true;
                break;
            case 4:
                PlayerController.canAim = true;
                break;
            case 5:
                PlayerController.canLaunch = true;
                PlayerController.doneIntro = true;
                break;
        }

        if (lm.turn == 2) StartCoroutine(RunSpecial());
    }
}
