using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Endscreen : MonoBehaviour
{
    public void MainMenu()
    {
        //0 goes to the first scene in the build number order, which we set to main menu
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        //+1 to the build index makes sure that no matter what scene you're at - it'll play the next level relative to it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        //gets the build number of the current level and reloads that
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextLevel2()
    {
        SceneManager.LoadScene("First Build Test");
    }
}
