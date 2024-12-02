using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] TextMeshProUGUI yourRoll;
    [SerializeField] PlayerController playerController;

    /*    private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }*/

    /*    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Checks for Gacha scene and assigns inspector stuff using Find()");
            if (SceneManager.GetActiveScene().name == "GachaSceneBuild")
            {
                //currency = GameObject.Find
                currency.text = PlayerController.tunaPoints.ToString();
            }

        }*/

    public void Start()
    {
        playerController = GameObject.Find("Player Launcher").GetComponent<PlayerController>();
    }

    public void Roll()
    {
        Debug.Log("Pulling time! Good luck!");
        PlayerController.tunaPoints--;
        int random = Random.Range(0, 4);
        playerController.hatsUnlocked[random] = true;
        yourRoll.text = "You unlocked the " + playerController.hats[random].GetComponent<MeshRenderer>().enabled + " hat!";
    }
}
