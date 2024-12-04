using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Gacha : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] TextMeshProUGUI yourRoll;
    [SerializeField] PlayerController playerController;
    public GameObject mainCam;
    public GameObject eventSystem;

    [SerializeField] Animator gachaAnim;
    [SerializeField] Material[] ball = new Material[6];
    //help me


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
        if (playerController == null)
        {
            playerController = GameObject.Find("Player Launcher").GetComponent<PlayerController>();
            if (playerController == null)
            {
            camSetup();
            }
            else
            {
            mainCam.SetActive(false);
            eventSystem.SetActive(false);
            }
            
                currency.text = PlayerController.tunaPoints.ToString();
        }
        
    }

    public void camSetup()
    {
            mainCam.SetActive(true);
            eventSystem.SetActive(true);

    }

    public void Roll()
    {

        if (PlayerController.tunaPoints > 0)
        {
            gachaAnim.SetTrigger("Play");

            Debug.Log("Pulling time! Good luck!");
            PlayerController.tunaPoints--;
            currency.text = PlayerController.tunaPoints.ToString();

            StartCoroutine(RevealDelay());
            
        }
        
    }

    IEnumerator RevealDelay()
    {
        yield return new WaitForSeconds(3.75f);
    
        int random = Random.Range(0, 4);
        playerController.hatsUnlocked[random] = true;
        yourRoll.text = "You unlocked the " + playerController.hats[random].name + " hat!";
        gachaAnim.ResetTrigger("Play");

    }
}
