using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioClip[] sfxClips;

    public static AudioManager Instance;
	private void Awake()
	{
		if(Instance!=null)
		{
            Destroy(gameObject);
		}
        else
		{
            DontDestroyOnLoad(gameObject);
		}
	}
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
