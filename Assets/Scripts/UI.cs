using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;
    public AudioClip blink;
    public AudioSource soundsUI;

/* this was for prototype testing
    [Header("For Testing")]
    public GameObject playerPiece;
    public GameObject enemyPiece;
    public Transform piecePlacement;*/

    public void UpdateScoreText()
    {
        playerScoreText.text = "Comfy Cats: " + ScoreTracker.playerScore;
        enemyScoreText.text = "Dingus Dogs: " + ScoreTracker.enemyScore;
    }

    /* this was for prototype testing
    public void MakePlayerPiece()
    {
        Instantiate(playerPiece, new Vector3(0.1f, 0.25f, 2.2f), transform.rotation);
    }
    public void MakeEnemyPiece()
    {
        Instantiate(enemyPiece, new Vector3(0.1f, 0.25f, 2.2f), transform.rotation);
    }*/

    public void MainMenu()
    {
        //0 goes to the first scene in the build number order, which we set to main menu
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        //+1 to the build index makes sure that no matter what scene you're at - it'll play the next level relative to it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        //gets the build number of the current level and reloads that
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayUISound()
    {
        soundsUI.PlayOneShot(blink);
    }
}
