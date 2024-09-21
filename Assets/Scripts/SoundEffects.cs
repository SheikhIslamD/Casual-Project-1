using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [Header("Sounds go here")]
    public AudioClip slip;
    public AudioClip bonk;
    public AudioClip boom;
    public AudioClip[] meows;

    [SerializeField] private AudioSource soundObject;
    //this makes this script into a singleton - meaning only one of this can exist at a time and any script can now call this by just doing SoundEffects.instance... very epic
    //holy shit this also allows any script to reference its public variables like the sound clips listed above, they just gotta do like SoundEffects.instance.meow YEAHHHH
    public static SoundEffects instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //this is the function that other scripts will use to play effects
    //it asks for what sound to play, where to spawn it in at, how loud it should be, and what object will be its parent
    public void PlaySoundEffect(AudioClip audioClip, Transform spawnLocation, float Volume, Transform parent)
    {
        AudioSource audioSource = Instantiate(soundObject, spawnLocation.position, Quaternion.identity, parent);
        audioSource.clip = audioClip;
        audioSource.volume = Volume;
        audioSource.Play();

        //destroy the object it made to play the sound with, after the sound is done
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    //adding an array read version of playing sounds in case we want variants and randomness
        public void PlaySoundEffectRandom(AudioClip[] audioClip, Transform spawnLocation, float Volume, Transform parent)
    {
        //roll the random here, to make it choose from the array of clips
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundObject, spawnLocation.position, Quaternion.identity, parent);
        audioSource.clip = audioClip[rand];
        audioSource.volume = Volume;
        audioSource.Play();

        //destroy the object it made to play the sound with, after the sound is done
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

}
