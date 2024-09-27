using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Reference to the Canvas to be shown
    public GameObject targetCanvas;

    // Reference to the Canvas to be hidden (the previous Canvas)
    public GameObject previousCanvas;

    // Method to show the target Canvas and hide the previous one
    public void ShowCanvas()
    {
        // Hide the previous Canvas if it exists
        if (previousCanvas != null)
        {
            previousCanvas.SetActive(false);
        }

        // Show the target Canvas if it exists
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(true);
        }
    }
}