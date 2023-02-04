
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class Audio : MonoBehaviour
{
    public static AudioSource source;
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    public static void Play(AudioClip clip, float volume = 1, float pitch = 1)
    {
        source.pitch = pitch;
        source.PlayOneShot(clip, volume);
    }

}
