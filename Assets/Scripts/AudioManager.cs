using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource musicSource;
    
    public AudioSource sfxSource;
    
    public AudioClip backgroundMusic;
    public AudioClip playerFire;
    public AudioClip playerDie;
    public AudioClip enemyDie;
    public AudioClip lapComplete;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayBGM();
    }

    private void PlayBGM()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    
    public void PlaySfx(AudioClip sfxClip)
    {
        if (sfxSource != null && sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }
    
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
