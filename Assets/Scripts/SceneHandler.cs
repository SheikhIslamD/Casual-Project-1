using UnityEngine;
using UnityEngine.SceneManagement; // Include this namespace for scene management

public class SceneHandler : MonoBehaviour
{
    //audio stuff
    public AudioClip tunacanSound;
    public AudioClip blink;
    public AudioSource audioSource;

    // The root GameObject of the current scene to disable when loading the new scene
    public GameObject currentSceneRoot;
    // Where We're Going
    public string whereTo;

    // Function to load the new scene additively and hide the current scene
    public void LoadNewSceneAndHideCurrent()
    {
        switch (whereTo)
        {
            case "Menu":
                //0 goes to the first scene in the build number order, which we set to main menu
                SceneManager.LoadScene(0);
                break;
            case "Retry":
                //gets the build number of the current level and reloads that
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                //+1 to the build index will play next scene in build index order
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "Quit":
                Application.Quit();
                break;
        }

        // Disable the current scene root if it exists
        if (currentSceneRoot != null)
        {
            currentSceneRoot.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Current scene root is not assigned!");
        }
    }

    public void PlayUISound()
    {
        audioSource.PlayOneShot(blink);
    }

    public void PlayMenuSound()
    {
        audioSource.PlayOneShot(tunacanSound);
    }
}
