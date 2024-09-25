using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CloneCat : MonoBehaviour
{

    // Destroy the duplicated cats on collision
    void OnCollisionEnter(Collision collision)
    {
        if (LaunchAbility.canDestroy)
        {
            Debug.Log("Contact made with ");
            StartCoroutine(Destroy());
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.8f);

        Destroy(this.gameObject);
    }
}
