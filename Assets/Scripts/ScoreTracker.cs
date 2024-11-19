using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    //Testing a Github thing
    //Current flow of game data: 
    //ScoreZone counts scores -> stores them in ScoreTracker + tells ScoreTracker to update
    //ScoreTracker stores the counted scores + gets told to update -> tells UI to update
    //this way, nothing has to be assigned to the UI in inspector scene-by-scene

    public static int playerScore = 0;
    public static int enemyScore = 0;
    public string currentScene;
    public UI UI;


    //[Header("Autoassigned per-level")]
    //[SerializeField] ScoreZone scoreZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //dont need this since both are child of Core, just put it in inspector
        //UICanvas = GameObject.Find("UICanvas").GetComponent<UI>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //show scene name in inspector for debug purposes
        currentScene = scene.name;
        //reset scores as new level comes in
        playerScore = 0;
        enemyScore = 0;
        //assign the new scoreZone
        //scoreZone = GameObject.FindWithTag("Score Zone").GetComponent<ScoreZone>();
        //DON'T keep using if() statements here to load level-by-level enemy scores, this is for testing only rn
        //instead, have scores updated dynamically by counting enemy pieces since they can be moved
        //this needs to track player pieces as they score anyways, so use the same way as that but with enemy piece tag
/*        if (scene.name == "SheikhWorkScene")
        {
            enemyScore = 25;
        }*/
    }

    //ScoreZone uses this for live score updates
    public void UpdateScore()
    {
        UI.UpdateScoreText();
    }
}
