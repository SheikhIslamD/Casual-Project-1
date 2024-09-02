using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;

    [Header("For Testing")]
    public GameObject playerPiece;
    public GameObject enemyPiece;
    public Transform piecePlacement;

    public void UpdateScoreText()
    {
        playerScoreText.text = "Player: " + ScoreTracker.playerScore;
        enemyScoreText.text = "Enemy: " + ScoreTracker.enemyScore;
    }

    public void MakePlayerPiece()
    {
        Instantiate(playerPiece, new Vector3(0.1f, 0.25f, 2.2f), transform.rotation);
    }
    public void MakeEnemyPiece()
    {
        Instantiate(enemyPiece, new Vector3(0.1f, 0.25f, 2.2f), transform.rotation);
    }
}
