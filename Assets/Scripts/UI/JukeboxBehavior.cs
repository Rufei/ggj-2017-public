using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Beat
{
    public int beatInSong;
    public bool isPlayer1Firing;
    public bool isPlayer2Firing;
}

public class JukeboxBehavior : MonoBehaviour
{
    [System.Serializable]
    public class Library
    {
        public Sfx playerReady;
        public Sfx playerUnready;
        public Sfx allPlayersReady;
        public Music menuMusic;
        public Music gameMusic;
    }

    [System.Serializable]
    public class Sfx
    {
        public AudioClip clip;
    }

    [System.Serializable]
    public class Music
    {
        public AudioClip clip;
        public int beatsPerMinute;
        public int beatsPerMeasure = 4;
    }

    public Library lib;
    public AudioSource cameraAudioSourcePrefab;

    private AudioSource sfxSrc;
    private AudioSource musicSrc;
    private float fadeTimestamp = -1f;
    private float fadeDuration = 0f;
    private AudioClip clipToLoad;

    void Start()
    {
        if (sfxSrc == null)
        {
            sfxSrc = Instantiate(cameraAudioSourcePrefab, transform);
        }
        if (musicSrc == null)
        {
            musicSrc = Instantiate(cameraAudioSourcePrefab, transform);
            musicSrc.volume = 0.4f;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        sfxSrc.Stop();
        sfxSrc.clip = sfx.clip;
        sfxSrc.Play();
    }

    public void PlayMusic(Music music)
    {
        if (musicSrc.clip == music.clip)
        {
            if (musicSrc.isPlaying)
            {
                return;
            }
            musicSrc.Play();
        } else
        {
            musicSrc.Stop();
            musicSrc.clip = music.clip;
            musicSrc.Play();
        }
    }
}
