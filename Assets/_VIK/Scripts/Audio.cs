
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class Audio : MonoBehaviour
{
    public static AudioSource source;
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    
    public static void Play( AudioClip clip, float volume )
    {
        source.PlayOneShot( clip, volume );
    }
}
