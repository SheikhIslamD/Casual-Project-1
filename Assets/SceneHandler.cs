using UnityEngine;
using UnityEngine.SceneManagement; // Include this namespace for scene management

public class SceneHandler : MonoBehaviour
{
    //audio stuff
    public AudioClip tunacanSound;
    public AudioSource audioSource;

    // The name of the scene to load
    public string newSceneName;

    // The root GameObject of the current scene to disable when loading the new scene
    public GameObject currentSceneRoot;

    // Function to load the new scene additively and hide the current scene
    public void LoadNewSceneAndHideCurrent()
    {
        // Check if the new scene name is valid
        if (!string.IsNullOrEmpty(newSceneName))
        {
            // Load the new scene additively (i.e., without unloading the current scene)
            SceneManager.LoadScene(newSceneName);

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
        else
        {
            Debug.LogWarning("New scene name is not set!");
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void PlayMenuSound()
    {
        audioSource.PlayOneShot(tunacanSound);
    }
}
