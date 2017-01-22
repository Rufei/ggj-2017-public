using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseClickLoadsSceneByName : MonoBehaviour
{
    public string defaultSceneToLoad;

    void OnMouseDown()
    {
        if (!String.IsNullOrEmpty(defaultSceneToLoad))
        {
            LoadScene(defaultSceneToLoad);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
