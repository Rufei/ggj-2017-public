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
        [System.Serializable]
        public class WeaponBeats
        {
            public bool[] guitarBeats = new bool[8];
            public bool[] bassBeats = new bool[8];
            public bool[] drumBeats = new bool[8];
            public bool[] vocalBeats = new bool[8];
            public bool[] cymbalBeats = new bool[8];
        }

        public AudioClip clip;
        public bool requiresBeatMapping;
        public int beatsPerMinute;
        public int beatsPerMeasure = 4;
        public WeaponBeats weaponBeats;
    }

    [System.Serializable]
    public class Beat
    {
        public int beatInSong;
        public bool isPlayer1Firing;
        public bool isPlayer2Firing;
        public Beat(int beatInSong)
        {
            this.beatInSong = beatInSong;
            isPlayer1Firing = false;
            isPlayer2Firing = false;
        }
    }

    public static class CONST
    {
        public const float EARLY_INPUT_WINDOW_SECONDS = 0.05f;
        public const float LATE_INPUT_WINDOW_SECONDS = 0.05f;
        public const int WEAPON_BEAT_MAPPING_TOTAL_BEATS = 8;
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
    public Beat GetBeat()
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

        return currentBeats[thisBeat];
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

    private int[] GetCurrentBeats(float earlyWindow, float lateWindow)
    {
        float seconds = musicSrc.time;
        float beatsPerQuarterNote = (float)currentMusic.beatsPerMeasure / 4f;
        float beatsPerSecond = ((float)currentMusic.beatsPerMinute) / 60f;
        float earlyRaw = (Mathf.Max(seconds - earlyWindow, 0)) * beatsPerQuarterNote * beatsPerSecond;
        float raw = seconds * beatsPerQuarterNote * beatsPerSecond;
        float lateRaw = (Mathf.Min(seconds + lateWindow, currentMusic.clip.length)) * beatsPerQuarterNote * beatsPerSecond;
        return new int[] { (int)earlyRaw, (int)raw, (int)lateRaw };
    }

    // Returns true if the attack was registered, otherwise it returns false
    public bool RequestAttack(PlayerData attackingPlayer)
    {
        if (currentMusic == null || !currentMusic.requiresBeatMapping || !musicSrc.isPlaying)
        {
            return false;
        }

        int[] beats = GetCurrentBeats(CONST.EARLY_INPUT_WINDOW_SECONDS, CONST.LATE_INPUT_WINDOW_SECONDS);
        int earliestAttackBeat = int.MaxValue;

        foreach (int b in beats)
        {
            earliestAttackBeat = Mathf.Min(earliestAttackBeat,
                GetSoonestPossibleWeaponBeat(b, attackingPlayer.characterType));
        }

        if (!currentBeats.ContainsKey(earliestAttackBeat))
        {
            Beat newBeat = new Beat(earliestAttackBeat);
            currentBeats[earliestAttackBeat] = newBeat;
        }

        Beat beat = currentBeats[earliestAttackBeat];
        if (beat == null)
        {
            return false;
        }

        // Everything checks out, register the attack
        if (attackingPlayer.playerNum == 0)
        {
            beat.isPlayer1Firing = true;
        }
        if (attackingPlayer.playerNum == 1)
        {
            beat.isPlayer2Firing = true;
        }
        currentBeats[earliestAttackBeat] = beat;

        return true;
    }

    private int GetSoonestPossibleWeaponBeat(int beat, Character.CHARTYPE weapon)
    {
        bool[] weaponBeats = null;
        switch(weapon)
        {
            case Character.CHARTYPE.CHAR_GUITAR:
                weaponBeats = currentMusic.weaponBeats.guitarBeats;
                break;
            case Character.CHARTYPE.CHAR_BASS:
                weaponBeats = currentMusic.weaponBeats.bassBeats;
                break;
            case Character.CHARTYPE.CHAR_DRUM:
                weaponBeats = currentMusic.weaponBeats.drumBeats;
                break;
            case Character.CHARTYPE.CHAR_VOCAL:
                weaponBeats = currentMusic.weaponBeats.vocalBeats;
                break;
            case Character.CHARTYPE.CHAR_CYMBAL:
                weaponBeats = currentMusic.weaponBeats.cymbalBeats;
                break;
            default:
                return int.MaxValue;
        }
        
        for (int i = beat; i < beat + CONST.WEAPON_BEAT_MAPPING_TOTAL_BEATS; i++)
        {
            if (weaponBeats[i % CONST.WEAPON_BEAT_MAPPING_TOTAL_BEATS])
            {
                return i;
            }
        }

        return int.MaxValue;
    }
}
