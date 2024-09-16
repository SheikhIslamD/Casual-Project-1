using UnityEditor.Playables;
using UnityEngine;

public class LaunchAbility : MonoBehaviour
{
    PlayerController pc;
    static bool hasTriggered;

    public string ability;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

    public void UseLaunchAbility()
    {
        Debug.Log("Running Ability Now");

        if (!hasTriggered)
        {
            switch (ability)
            {
                // Target Friendly
                case "Zoom":
                    Zoom();
                    break;
                // Target Enemy 
                case "Divide":
                    Divide();
                    break;
            }
        }
    }

    void Zoom()
    {
        PlayerController.Launch();
    }
}
