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
        public Music guitarGameMusic;
        public Music bassGameMusic;
        public Music drumGameMusic;
        public Music vocalGameMusic;
        public Music cymbalGameMusic;
        public Music constantGameMusic;
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
            public bool[] guitarBeats = new bool[16];
            public bool[] bassBeats = new bool[16];
            public bool[] drumBeats = new bool[16];
            public bool[] vocalBeats = new bool[16];
            public bool[] cymbalBeats = new bool[16];
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
//        public const float EARLY_INPUT_WINDOW_SECONDS = 0.05f;
//        public const float LATE_INPUT_WINDOW_SECONDS = 0.05f;
        public const float EARLY_INPUT_WINDOW_SECONDS = 0.09375f;
        public const float LATE_INPUT_WINDOW_SECONDS = 0.09375f;
        public const float MUSIC_VOLUME_DEFAULT = 0.4f;
        public const float MUSIC_VOLUME_MUTED = 0.0f;
        public const float MUSIC_VOLUME_LOUD = 0.6f;
    }

    public Library lib;
    public AudioSource audioSourcePrefab;

    private AudioSource sfxSrc;
    private AudioSource musicSrc;
    private AudioSource guitarSrc;
    private AudioSource bassSrc;
    private AudioSource drumSrc;
    private AudioSource vocalSrc;
    private AudioSource cymbalSrc;
    private Music currentMusic;
    private Dictionary<int, Beat> currentBeats;

    private Character.CHARTYPE playerOneType;
    private Character.CHARTYPE playerTwoType;
    private int lastHandledBeat = -1;
    private int playerOneMuteBeat = -1;
    private int playerTwoMuteBeat = -1;

    void Start()
    {
        currentBeats = new Dictionary<int, Beat>();
        if (sfxSrc == null)
        {
            sfxSrc = Instantiate(audioSourcePrefab, transform);
        }
        if (musicSrc == null)
        {
            musicSrc = Instantiate(audioSourcePrefab, transform);
            musicSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
        }
        if (guitarSrc == null)
        {
            guitarSrc = Instantiate(audioSourcePrefab, transform);
            guitarSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            guitarSrc.clip = lib.guitarGameMusic.clip;
        }
        if (bassSrc == null)
        {
            bassSrc = Instantiate(audioSourcePrefab, transform);
            bassSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            bassSrc.clip = lib.bassGameMusic.clip;
        }
        if (drumSrc == null)
        {
            drumSrc = Instantiate(audioSourcePrefab, transform);
            drumSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            drumSrc.clip = lib.drumGameMusic.clip;
        }
        if (vocalSrc == null)
        {
            vocalSrc = Instantiate(audioSourcePrefab, transform);
            vocalSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            vocalSrc.clip = lib.vocalGameMusic.clip;
        }
        if (cymbalSrc == null)
        {
            cymbalSrc = Instantiate(audioSourcePrefab, transform);
            cymbalSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            cymbalSrc.clip = lib.cymbalGameMusic.clip;
        }
    }

    void Update()
    {
        JukeboxBehavior.Beat beat = GetBeat();
        if (beat != null && lastHandledBeat != beat.beatInSong) {
            //Debug.Log("Player  " + (playerScript.playerNum + 1) + " " + "FIRING at " + Time.deltaTime);
            if (beat.isPlayer1Firing)
            {
                GetAudioSource(playerOneType).volume = CONST.MUSIC_VOLUME_LOUD;
                if (playerOneType.Equals(Character.CHARTYPE.CHAR_CYMBAL)) {
                    playerOneMuteBeat = GetSoonestPossibleWeaponBeat(beat.beatInSong + 1, playerOneType);
                } else
                {
                    playerOneMuteBeat = GetSoonestPossibleWeaponBeat(beat.beatInSong + 1, playerOneType) + 1;
                }
            }
            if (beat.isPlayer2Firing)
            {
                GetAudioSource(playerTwoType).volume = CONST.MUSIC_VOLUME_LOUD;
                if (playerTwoType.Equals(Character.CHARTYPE.CHAR_CYMBAL))
                {
                    playerTwoMuteBeat = GetSoonestPossibleWeaponBeat(beat.beatInSong + 1, playerTwoType);
                } else
                {
                    playerTwoMuteBeat = GetSoonestPossibleWeaponBeat(beat.beatInSong + 1, playerTwoType) + 1;
                }
            }
            lastHandledBeat = GetCurrentBeat();
        }

        if (currentMusic != null)
        {
            int cur = GetCurrentBeat();
            if (playerOneMuteBeat == cur)
            {
                GetAudioSource(playerOneType).volume = CONST.MUSIC_VOLUME_MUTED;
            }
            if (playerTwoMuteBeat == cur)
            {
                GetAudioSource(playerTwoType).volume = CONST.MUSIC_VOLUME_MUTED;
            }
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
            guitarSrc.Stop();
            bassSrc.Stop();
            drumSrc.Stop();
            vocalSrc.Stop();
            cymbalSrc.Stop();
            musicSrc.clip = music.clip;
            currentMusic = music;
            musicSrc.Play();
        }
    }

    public void PlayGameMusic(Character.CHARTYPE playerOne, Character.CHARTYPE playerTwo)
    {
        if (musicSrc.clip == lib.constantGameMusic.clip)
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
            guitarSrc.Stop();
            bassSrc.Stop();
            drumSrc.Stop();
            vocalSrc.Stop();
            cymbalSrc.Stop();
            musicSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            guitarSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            bassSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            drumSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            vocalSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            cymbalSrc.volume = CONST.MUSIC_VOLUME_DEFAULT;
            GetAudioSource(playerOne).volume = CONST.MUSIC_VOLUME_MUTED;
            GetAudioSource(playerTwo).volume = CONST.MUSIC_VOLUME_MUTED;
            musicSrc.clip = lib.constantGameMusic.clip;
            currentMusic = lib.constantGameMusic;
            playerOneType = playerOne;
            playerTwoType = playerTwo;
            musicSrc.Play();
            guitarSrc.Play();
            bassSrc.Play();
            drumSrc.Play();
            vocalSrc.Play();
            cymbalSrc.Play();
        }
    }

    public AudioSource GetAudioSource(Character.CHARTYPE type)
    {
        switch (type)
        {
            case Character.CHARTYPE.CHAR_GUITAR:
                return guitarSrc;
            case Character.CHARTYPE.CHAR_BASS:
                return bassSrc;
            case Character.CHARTYPE.CHAR_DRUM:
                return drumSrc;
            case Character.CHARTYPE.CHAR_VOCAL:
                return vocalSrc;
            case Character.CHARTYPE.CHAR_CYMBAL:
                return cymbalSrc;
            default:
                return null;
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
            earliestAttackBeat = Mathf.Min(earliestAttackBeat,GetSoonestPossibleWeaponBeat(b, attackingPlayer.characterType));
        }

        bool isValid = false;
        foreach (int b in beats)
        {
            if (b == earliestAttackBeat)
            {
                isValid = true;
            }
        }

        if (!isValid)
        {
            return false;
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
        
        for (int i = beat; i < beat + weaponBeats.Length; i++)
        {
            if (weaponBeats[i % weaponBeats.Length])
            {
                return i;
            }
        }

        return int.MaxValue;
    }
}
