using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource _source;
    private AudioSource _musicSource;

    private void Awake()
    {

        _source = GetComponent<AudioSource>();
        _musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        // assign volumes
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
        
    }

    public void PlaySound(AudioClip sound)
    {
        _source.PlayOneShot(sound); //only once
    }

    public void ChangeSoundVolume(float change)
    {
        ChangeSourceVolume(1, "soundVolume", change, _source);
    }


    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        //assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //save
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }

    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", change, _musicSource);
    }

    public void PauseMusic()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Pause();  // Pauses the main music
        }
    }

    public void ResumeMusic()
    {
        _musicSource.UnPause();  // Resumes the paused main music
    }
}
