using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioClip[] sfxClips;


	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonHoverSound()
	{
        sfxAudioSource.PlayOneShot(sfxClips[0]);
	}

    public void PlayButtonClickSound()
    {
        sfxAudioSource.PlayOneShot(sfxClips[1]);
    }
}
