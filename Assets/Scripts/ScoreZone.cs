using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    public ScoreTracker scoreTracker;
    //setting this up to have scorezone analyze tag triggers
    //if no work, swap to individual pieces doing it instead

    private void Awake()
    {
        scoreTracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Piece"))
        {
            ScoreTracker.playerScore++;
        }

        if (other.CompareTag("Enemy Piece"))
        {
            ScoreTracker.enemyScore++;
        }
        scoreTracker.UpdateScore();
        Debug.Log("Piece entered");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player Piece"))
        {
            ScoreTracker.playerScore--;
        }

        if (other.CompareTag("Enemy Piece"))
        {
            ScoreTracker.enemyScore--;
        }
        scoreTracker.UpdateScore();
        Debug.Log("Piece exited");
    }
}
