using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currency;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Calls on Scene Loaded");
        if (SceneManager.GetActiveScene().name == "GachaSceneBuild")
        {
            //currency = GameObject.Find
            currency.text = PlayerController.tunaPoints.ToString();
        }

    }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
