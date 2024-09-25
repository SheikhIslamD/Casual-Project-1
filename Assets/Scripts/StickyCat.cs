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
            Debug.Log("Merging"); 
            collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            Debug.Log("rb asleep");

            Collider col = collision.collider;
            Debug.Log(col);
            Collider newCol = this.gameObject.AddComponent<Collider>();
            Debug.Log(newCol);
            newCol = col;

            collision.collider.enabled = false;
        }
    }
}
