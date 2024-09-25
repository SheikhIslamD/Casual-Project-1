using UnityEngine;

public class SoundEffectTrigger : MonoBehaviour
{
    //All trigger scenarios for any object that this script is attached to, to react accordingly
    //Tags are super important here
    private void OnCollisionEnter(Collision collision)
    {
        //this line will play a bonk for any object this script is on hitting another - whether its collision pieces or objects
        SoundEffects.instance.PlaySoundEffect(SoundEffects.instance.bonk, transform, 1, null);
        Debug.Log("A piece hit somethin, BONK");

        //Checks if this is a Cat piece, if so, then all Cat piece sound playing situations will apply in here
        if (CompareTag("Player Piece"))
        {
            //meow on hits with collision cats
            if (collision.gameObject.CompareTag("Player Piece"))
            SoundEffects.instance.PlaySoundEffectRandom(SoundEffects.instance.meows, transform, 1, transform);
            Debug.Log("A cat hit a cat, MEWO");
        }
        
        //Dog piece version
        if (CompareTag("Enemy Piece"))
        {
            //vineboom on cats hitting it
            if (collision.gameObject.CompareTag("Player Piece"))
            SoundEffects.instance.PlaySoundEffectRandom(SoundEffects.instance.barks, transform, 1, transform);
            Debug.Log("A cat hit a dog... BOOM");
        }
    }
}
