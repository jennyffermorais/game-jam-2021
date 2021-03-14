using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusicaDeFundo;


    public AudioClip[] musicaDeFundo;
    // Start is called before the first frame update
    void Start()
    {
        AudioClip musicaDeFundoDessaFase = musicaDeFundo[0];
        audioSourceMusicaDeFundo.clip = musicaDeFundoDessaFase;
        audioSourceMusicaDeFundo.loop = true;
        audioSourceMusicaDeFundo.Play();
    }

    
}
