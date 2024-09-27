using UnityEngine;
using UnityEngine.Audio;

public class StickyCat : MonoBehaviour
{
    public bool stickActive;
    public PhysicsMaterial sticky;
    void OnCollisionEnter(Collision collision)
    {
        if (stickActive)
        {
            Debug.Log("Contact made with Sticky");

            if (collision.collider.CompareTag("Player Piece") || collision.collider.CompareTag("Enemy Piece"))
            {
                Debug.Log("Giving new material");
                Collider col = collision.collider;
                col.material = sticky;
            }
        }
    }
}
