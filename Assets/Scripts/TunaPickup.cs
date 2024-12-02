using UnityEngine;

public class TunaPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Piece")
        {
            PlayerController.tunaPoints++;
            SoundEffects.instance.PlaySoundEffect(SoundEffects.instance.pickup, transform, 1, transform);
            Destroy(gameObject);
        }
    }
}
