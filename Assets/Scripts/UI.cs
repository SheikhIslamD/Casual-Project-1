using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class UI : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;

    public AudioClip blink;
    public AudioSource soundsUI;

    public GameObject cataloguePopup;
    public bool gameIsPaused = false;
    public GameObject pauseMenu;

    InputAction pauseKey;

    void Awake()
    {
        pauseMenu.SetActive(false);
        pauseKey = InputSystem.actions.FindAction("Pause");
    }

    void Update()
    {
        if (pauseKey.WasPerformedThisFrame())
        {
            PauseGame();
            playButtonSound();
            Debug.Log("pressed pause key");
        }
    }    
    
    public void UpdateScoreText()
    {
        playerScoreText.text = "Comfy Cats: " + ScoreTracker.playerScore;
        enemyScoreText.text = "Dingus Dogs: " + ScoreTracker.enemyScore;
    }
    public void MainMenu()
    {
        //0 goes to the first scene in the build number order, which we set to main menu
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        //gets the build number of the current level and reloads that
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        //closes game
        Application.Quit();
    }

    public void Catalogue()
    {
        //pops up current pieces in level catalogue as assigned in the levelmanager, closes when re-clicked
        if (cataloguePopup.activeSelf)
        {
            cataloguePopup.SetActive(false);
        }
        else 
        {
            cataloguePopup.SetActive(true);
        }
    }

    public void playButtonSound()
    {
        //use this function to play sounds for everything in the ui
        soundsUI.PlayOneShot(blink);
    }

    public void PauseGame()
    {
        //if the level isn't over and on the endscreen already, then the game will pause
        //if already paused, it'll resume
        if (!LevelManager.levelOver)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                //makes sure the catalogue won't already be popped up
                cataloguePopup.SetActive(false);
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
                Debug.Log("Game is paused");
            }
        }
    }

    public void Resume()
    {
        if (gameIsPaused)
        {
            cataloguePopup.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            Debug.Log("Game is resumed");
        }
    }

    public void SwapHats()
    {
        PlayerController.hatEquipped++;
        if (PlayerController.hatEquipped >= 4)
        {
            PlayerController.hatEquipped = 0;
        }
    }
    public void Skip()
    {
        PlayerController.canMove = true;
        PlayerController.canAim = true;
        PlayerController.canLaunch = true;
        PlayerController.doneIntro = true;
    }



    /* this was for prototype testing
        [Header("For Testing")]
        public GameObject playerPiece;
        public GameObject enemyPiece;
        public Transform piecePlacement;*/

    /* this was for prototype testing
    public void MakePlayerPiece()
    {
        Instantiate(playerPiece, new Vector3(0.1f, 0.25f, 2.2f), transform.rotation);
    }
    public void MakeEnemyPiece()
    {
        Instantiate(enemyPiece, new Vector3(0.1f, 0.25f, 2.2f), transform.rotation);
    }*/
}
