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
}
