using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Public repositiory to store level information
    public int turn = 0;
    public int turnMax;
    public int scoreToBeat;

    public GameObject[] prefabPuck;

    void ScoreRound()
    {
        //Compare the player score to enemy score
        //If high enough clear, too low fail
        //
        
    }
}
