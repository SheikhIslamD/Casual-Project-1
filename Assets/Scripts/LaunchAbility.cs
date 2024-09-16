using UnityEditor.Playables;
using UnityEngine;

public class LaunchAbility : MonoBehaviour
{
    Rigidbody rb;
    static bool hasTriggered;

    public string ability;

    private void Awake()
    {
        rb= GetComponent<Rigidbody>();
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
                    rb.AddForce(Vector3.forward * 20, ForceMode.Impulse);
                    hasTriggered = true;
                    break;
                // Target Enemy 
                case "Divide":
                    Divide();
                    break;
            }
        }
    }

    void Divide()
    {
        
    }
}
