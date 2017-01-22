using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoadsSceneByName : MonoBehaviour
{
    public string defaultSceneToLoad;

    void OnClick()
    {
        if (!String.IsNullOrEmpty(defaultSceneToLoad))
        {
            LoadScene(defaultSceneToLoad);
        }
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
