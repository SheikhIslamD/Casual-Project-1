using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Public repositiory to store level information
    public int turn = 0;
    public int turnMax;

    // Info for End of Level UI
    public int star2Diff, star3Diff;
    Image medal;
    public Sprite gold, silver, bronze, black;
    TextMeshProUGUI winText, loseText, tiedText, scoreText;
    Canvas inGameCanvas, endCanvas;

    // Arrays to track all active pieces in scene
    public GameObject[] prefabPlayerPuck;
    public GameObject[] prefabEnemyPuck;

    private void Awake()
    {
        turnMax = prefabPlayerPuck.Length;
        Debug.Log(turnMax + " Total turns");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ScoreRound()
    {
        //Compare the player score to enemy score
        //For positive feedback loop maybe use a "star" system. i.e. 1 star for win by 1, 2 starts for win by 2, and 3 for 3+
        inGameCanvas.enabled = false;
        endCanvas.enabled = true;

        // Show final score
        scoreText.text = "Score: " + (ScoreTracker.playerScore - ScoreTracker.enemyScore);

        // Prep text fields
        winText.enabled = false;
        tiedText.enabled = false;
        loseText.enabled = false;

        // If player score meets score requirements, allocate stars
        if (ScoreTracker.playerScore >= ScoreTracker.enemyScore)
        {
            if (ScoreTracker.playerScore - ScoreTracker.enemyScore >= star2Diff)
            {
                winText.enabled = true;
                if (ScoreTracker.playerScore - ScoreTracker.enemyScore >= star3Diff)
                {
                    // UI 3 star
                    medal.sprite = gold;
                    return;
                }
                // UI 2 star
                medal.sprite = silver;
                return;
            }            
            // UI 1 star
            tiedText.enabled = true;
            medal.sprite = bronze;
            return;
        }

        // UI lose Text
        loseText.enabled = true;
        medal.sprite = black;
        return;
    }

    // Check all pieces in scene and ensure they've stopped moving
    public bool VelocityZero()
    {
        foreach (GameObject dog in prefabEnemyPuck)
        {
            if (dog.GetComponent<Rigidbody>().linearVelocity.magnitude > 0.01) return false;
        }
        foreach (GameObject cat in prefabPlayerPuck)
        {
            if (cat.GetComponent<Rigidbody>().linearVelocity.magnitude > 0.01) return false;
        }
        return true;
    }

    // Called once in every scene, gets the components neccessary for script functions
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Calls on Scene Loaded");
        turn = 0;
        PlayerController.playing = true;

        inGameCanvas = GameObject.Find("HUD").GetComponent<Canvas>();
        endCanvas = GameObject.Find("End Screen").GetComponent<Canvas>();

        inGameCanvas.enabled = true;
        endCanvas.enabled = false;

        medal = GameObject.Find("Medal").GetComponent<Image>();
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        loseText = GameObject.Find("LoseText").GetComponent<TextMeshProUGUI>();
        tiedText = GameObject.Find("TiedText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
}
