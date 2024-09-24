using UnityEngine;
using UnityEngine.Audio;

public class StickyCat : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Contact made with ");
        Debug.Log(collision.collider);
        if (collision.collider.CompareTag("Player Piece") || collision.collider.CompareTag("Enemy Piece"))
        {
            Debug.Log("Assigning joint");
            collision.gameObject.AddComponent<FixedJoint>();
            collision.gameObject.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
        }
    }
}
