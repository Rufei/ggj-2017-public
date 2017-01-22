using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaysMenuMusic : MonoBehaviour
{
    private JukeboxBehavior jukebox;
    
    void Start ()
    {
        jukebox = GameObject.Find("/Jukebox").GetComponent<JukeboxBehavior>();
    }

    void Update()
    {
        jukebox.PlayMusic(jukebox.lib.menuMusic);
    }
}
