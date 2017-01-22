using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public bool requiresBeatMapping;
        public int beatsPerMinute;
        public int beatsPerMeasure = 4;
    }

    [System.Serializable]
    public class Beat
    {
        public int beatInSong;
        public bool isPlayer1Firing;
        public bool isPlayer2Firing;
    }

    public Library lib;
    public AudioSource cameraAudioSourcePrefab;

    private AudioSource sfxSrc;
    private AudioSource musicSrc;
    private Music currentMusic;
    private Dictionary<int, Beat> currentBeats;

    void Start()
    {
        currentBeats = new Dictionary<int, Beat>();
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

    //void FixedUpdate()
    //{
    //    if (currentMusic != null && currentMusic.requiresBeatMapping && musicSrc.isPlaying)
    //    {
    //        Debug.Log("[Beat: " + GetCurrentBeat() + "]\t[Time: " + musicSrc.time + "]\t[Samples: " + musicSrc.timeSamples);
    //    }
    //}

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
        }
        else
        {
            musicSrc.Stop();
            musicSrc.clip = music.clip;
            currentMusic = music;
            musicSrc.Play();
        }
    }

    // Most external facing func. Pound this in Update() to receive it on the soonest possible frame.
    public Beat ConsumeBeat()
    {
        if (currentMusic == null || !currentMusic.requiresBeatMapping || !musicSrc.isPlaying)
        {
            return null;
        }

        int thisBeat = GetCurrentBeat();
        if (!currentBeats.ContainsKey(thisBeat))
        {
            return null;
        }

        Beat beat = currentBeats[thisBeat];
        currentBeats.Remove(thisBeat);
        return beat;
    }

    private int GetCurrentBeat()
    {
        float seconds = musicSrc.time;
        float beatsPerQuarterNote = (float)currentMusic.beatsPerMeasure / 4f;
        float beatsPerSecond = ((float)currentMusic.beatsPerMinute) / 60f;
        float raw = seconds * beatsPerQuarterNote * beatsPerSecond;
        int cur = (int)raw;
        return cur;
    }
}
