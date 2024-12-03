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
    TextMeshProUGUI resultText, scoreText;
    Canvas inGameCanvas, endCanvas;

    // Arrays to track all active pieces in scene
    public GameObject[] prefabPlayerPuck;
    public GameObject[] prefabEnemyPuck;

    //public static variable to let other scripts know that level is over
    public static bool levelOver = false;

    private void Awake()
    {
        turnMax = prefabPlayerPuck.Length;
        Debug.Log(turnMax + " Total turns");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ScoreRound()
    {
        //For positive feedback loop maybe use a "medal" system. i.e. 1 star for win by 1, 2 starts for win by 2, and 3 for 3+
        inGameCanvas.enabled = false;
        endCanvas.enabled = true;

        // Show final score
        scoreText.text = "Score: " + (ScoreTracker.playerScore - ScoreTracker.enemyScore);

        // If player score meets score requirements, allocate stars
        if (ScoreTracker.playerScore >= ScoreTracker.enemyScore)
        {
            if (ScoreTracker.playerScore - ScoreTracker.enemyScore >= star2Diff)
            {
                if (ScoreTracker.playerScore - ScoreTracker.enemyScore >= star3Diff)
                {
                    // UI gold medal
                    resultText.text = "Amazing!!!";
                    medal.sprite = gold;
                    return;
                }
                // UI silver medal
                resultText.text = "Pretty Good!";
                medal.sprite = silver;
                return;
            }
            // UI bronze medal
            resultText.text = "Not Bad";
            medal.sprite = bronze;
            return;
        }

        // UI lose Text
        resultText.text = "Oh No!";
        medal.sprite = black;
        levelOver = true;
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
        levelOver = false;
        //recount the array to have the right turns
        turnMax = prefabPlayerPuck.Length;
        turn = 0;
        if (PlayerController.doneIntro)
        {
            PlayerController.playing = true;
        }
        Debug.Log("playing state set");

        if (SceneManager.GetActiveScene().name != "Intro" )
        {
            if (SceneManager.GetActiveScene().name != "Introduction")
            {
            Debug.Log("this is a non-intro scene, allowing all play variables");
            PlayerController.doneIntro = true;
            PlayerController.canMove = true;
            PlayerController.canAim = true;
            PlayerController.canLaunch = true;
            }
        }

        inGameCanvas = GameObject.Find("HUD").GetComponent<Canvas>();
        endCanvas = GameObject.Find("End Screen").GetComponent<Canvas>();

        inGameCanvas.enabled = true;
        endCanvas.enabled = false;

        medal = GameObject.Find("Medal").GetComponent<Image>();
        resultText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
}
