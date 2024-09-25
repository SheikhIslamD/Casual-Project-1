using System.Collections;
//using UnityEditor.Playables;
using UnityEngine;

public class LaunchAbility : MonoBehaviour
{
    public string ability;
    bool hasTriggered;

    float launchForce;
    Vector3 launchAngle;

    public GameObject copy;
    public static bool canDestroy;

    public Material clear;
    Material mat;

    public void UseLaunchAbility(Vector3 currentAngle, float currentForce)
    {
        Debug.Log("Running Ability Now");

        launchAngle = currentAngle;
        launchForce = currentForce;

        if (!hasTriggered)
        {
            switch (ability)
            {
                // Double Dash
                case "Zoom":
                    Zoom();
                    break;
                // Spawn temporary clones 
                case "Multiply":
                    Multiply();
                    break;
                // Become Untouchable
                case "Vanish":
                    Vanish();
                    break;
            }
        }
    }

    void Zoom()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(launchAngle * launchForce, ForceMode.Impulse);

        hasTriggered = true;
    }

    void Multiply()
    {
        hasTriggered = true;
        GameObject clone1 = Instantiate(copy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(-90f, 180f, transform.rotation.z + 30)));
        GameObject clone2 = Instantiate(copy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(-90f, 180f, transform.rotation.z - 30)));

        clone1.GetComponent<Rigidbody>().AddForce(new Vector3(launchAngle.x + Mathf.Cos(0.3f), 0, launchAngle.z + Mathf.Sin(0.3f)) * launchForce, ForceMode.Impulse);
        clone2.GetComponent<Rigidbody>().AddForce(new Vector3(launchAngle.x - Mathf.Cos(0.3f), 0, launchAngle.z - Mathf.Sin(0.3f)) * launchForce, ForceMode.Impulse);
        canDestroy = false;

        StartCoroutine(NowDestroy());
    }

    IEnumerator NowDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        canDestroy = true;
    }

    void Vanish()
    {
        hasTriggered = true;
        Physics.IgnoreLayerCollision(9, 6);
        Physics.IgnoreLayerCollision(9, 7);
        Physics.IgnoreLayerCollision(9, 8);

        Material temp = GetComponent<MeshRenderer>().material;

        GetComponent<Renderer>().material = clear;

        StartCoroutine(Appear(temp));
    }

    IEnumerator Appear(Material normal)
    {
        yield return new WaitForSeconds(0.75f);


        Physics.IgnoreLayerCollision(9, 6, false);
        Physics.IgnoreLayerCollision(9, 7, false);
        Physics.IgnoreLayerCollision(9, 8, false);

        GetComponent<Renderer>().material = normal;
    }

}
