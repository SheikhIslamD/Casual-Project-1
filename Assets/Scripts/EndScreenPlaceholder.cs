using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndscreenPlaceholder : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("UI Main Menu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Test Level 2");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel2()
    {
        SceneManager.LoadScene("First Build Test");
    }
}
