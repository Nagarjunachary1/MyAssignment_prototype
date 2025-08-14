using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject LC_obj;
    void Awake()
    {
        instance = this;
    }

    public void LevelComplete()
    {
        LC_obj.SetActive(true);
    }


    public void LoadNewScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }



}
