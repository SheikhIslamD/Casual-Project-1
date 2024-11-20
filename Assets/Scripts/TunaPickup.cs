using UnityEngine;

public class TunaPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Piece")
        {
            PlayerController.tunaPoints++;
            Destroy(gameObject);
        }
    }
}
