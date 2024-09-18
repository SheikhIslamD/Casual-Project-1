using System.Collections;
using UnityEditor.Playables;
using UnityEngine;

public class LaunchAbility : MonoBehaviour
{
    public string ability;
    bool hasTriggered;

    float launchForce;
    Vector3 launchAngle;

    Rigidbody rb;

    public GameObject holocat;
    bool checkingClone;

    public void UseLaunchAbility(Vector3 currentAngle, float currentForce)
    {
        Debug.Log("Running Ability Now");

        launchAngle = currentAngle;
        launchForce = currentForce;

        if (!hasTriggered)
        {
            switch (ability)
            {
                // Target Friendly
                case "Zoom":
                    Zoom();
                    break;
                // Target Enemy 
                case "Clone":
                    Clone();
                    break;
            }
        }
    }
    private void FixedUpdate()
    {
        if (checkingClone)
        {

        }
    }

    void Zoom()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(launchAngle * launchForce, ForceMode.Impulse);
        hasTriggered = true;
    }

    void Clone()
    {
        // Thinking we do like "Holograms," not worth points and disappear when velocity = 0
        GameObject clone1 = Instantiate(holocat, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(-90f, 180f, transform.rotation.z + 30)));
        GameObject clone2 = Instantiate(holocat, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(-90f, 180f, transform.rotation.z - 30)));

        clone1.GetComponent<Rigidbody>().AddForce(new Vector3(launchAngle.x - Mathf.Cos(60), 0, launchAngle.z - Mathf.Sin(60)) * launchForce, ForceMode.Impulse);
        clone2.GetComponent<Rigidbody>().AddForce(new Vector3(launchAngle.x - Mathf.Cos(30), 0, launchAngle.z - Mathf.Sin(30)) * launchForce, ForceMode.Impulse);

        checkingClone = true;
        hasTriggered = true;
    }

    IEnumerator DestroyClones()
    {
        yield return new WaitForSeconds(1.5f);

    }
}
