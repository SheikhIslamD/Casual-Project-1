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

    public void UseLaunchAbility(float currentForce)
    {
        Debug.Log("Running Ability Now");

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
        hasTriggered = true;

        // Apply a second launch to piece
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.up * launchForce, ForceMode.Impulse);
    }

    void Multiply()
    {
        hasTriggered = true;

        // Instantiate 2 smaller copies of this cat that destroy themselves on collision
        GameObject clone1 = Instantiate(copy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(-90f, 180f, transform.rotation.z + 30)));
        GameObject clone2 = Instantiate(copy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(-90f, 180f, transform.rotation.z - 30)));

        // 2 small clones launch off at new angles (30deg left & right of main piece)
        clone1.GetComponent<Rigidbody>().AddForce(clone1.transform.up * launchForce, ForceMode.Impulse);
        clone2.GetComponent<Rigidbody>().AddForce(clone2.transform.up * launchForce, ForceMode.Impulse);
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

        // Ignores collision with objects in scene (Like a ghost)
        // 8 is vanish cat, which ignores 6(other pieces) and 7(fruniture)
        Physics.IgnoreLayerCollision(8, 6);
        Physics.IgnoreLayerCollision(8, 7);

        // Visual, make piece clear to indicate ability to pass through objects
        Material temp = GetComponent<MeshRenderer>().material;
        GetComponent<Renderer>().material = clear;

        StartCoroutine(Appear(temp));
    }

    IEnumerator Appear(Material normal)
    {
        // After x time passes, can interact with colliders again.
        yield return new WaitForSeconds(0.8f);

        Physics.IgnoreLayerCollision(8, 6, false);
        Physics.IgnoreLayerCollision(8, 7, false);

        // Return material to normal
        GetComponent<Renderer>().material = normal;
    }

}
