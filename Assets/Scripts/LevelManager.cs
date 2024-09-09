using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Public repositiory to store level information
    public int turn = 0;
    public int turnMax;

    public int star2Diff;
    public int star3Diff;

    public int starTotal = 0;

    public GameObject[] prefabPlayerPuck;
    public GameObject[] prefabEnemyPuck;

    private void Start()
    {
        turnMax = prefabPlayerPuck.Length;
    }
    public void ScoreRound()
    {
        //Compare the player score to enemy score
        //For positive feedback loop maybe use a "star" system. i.e. 1 star for win by 1, 2 starts for win by 2, and 3 for 3+

        if (ScoreTracker.playerScore >= ScoreTracker.enemyScore)
        {
            if (ScoreTracker.playerScore >= ScoreTracker.enemyScore + star2Diff)
            {
                if (ScoreTracker.playerScore > ScoreTracker.enemyScore + star3Diff)
                {
                    // UI 3 star
                    starTotal = 3;
                    return;
                }
                // UI 2 star
                starTotal = 2;
                return;
            }
            // UI 1 star
            starTotal = 1;
            return;
        }

        // UI lose screen
        // Or outright just restart level?
        return;

    }
}
