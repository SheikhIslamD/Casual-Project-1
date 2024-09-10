using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Public repositiory to store level information
    public int star2Diff, star3Diff;
    Image star1, star2, star3;
    Color starColor = new Color32(195, 167, 18, 255);
    TextMeshProUGUI winText, loseText;
    Canvas inGameCanvas, endCanvas;

    public GameObject[] prefabPlayerPuck;
    public GameObject[] prefabEnemyPuck;

    private void Awake()
    {
        inGameCanvas = GameObject.Find("UI").GetComponent<Canvas>();
        endCanvas = GameObject.Find("End Screen").GetComponent<Canvas>();

        star1 = GameObject.Find("Star 1").GetComponent<Image>();
        star2 = GameObject.Find("Star 2").GetComponent<Image>();
        star3 = GameObject.Find("Star 3").GetComponent<Image>();

        winText = GameObject.Find("Win Text").GetComponent<TextMeshProUGUI>();
        loseText = GameObject.Find("Lose Text").GetComponent<TextMeshProUGUI>();
    }
    public void ScoreRound()
    {
        //Compare the player score to enemy score
        //For positive feedback loop maybe use a "star" system. i.e. 1 star for win by 1, 2 starts for win by 2, and 3 for 3+
        inGameCanvas.enabled = false;
        endCanvas.enabled = true;
        
        // If player score meets score requirements, allocate stars
        if (ScoreTracker.playerScore >= ScoreTracker.enemyScore)
        {
            // UI Win Text
            winText.enabled = true;

            if (ScoreTracker.playerScore - ScoreTracker.enemyScore >= star2Diff)
            {
                if (ScoreTracker.playerScore - ScoreTracker.enemyScore >= star3Diff)
                {
                    // UI 3 star
                    star3.color = starColor;
                }
                // UI 2 star
                star2.color = starColor;
            }
            // UI 1 star
            star1.color = starColor;
            return;
        }

        // UI lose Text
        loseText.enabled = false;
        // Or outright just restart level?
        return;
    }

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
}
