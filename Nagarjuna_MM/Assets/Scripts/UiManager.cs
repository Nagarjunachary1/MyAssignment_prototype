using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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


    public async void LoadNewScene(string _sceneName)
    {
        AudioEvents.ButtonClickSound();

        await Task.Delay(400);
        SceneManager.LoadSceneAsync(_sceneName);
    }



}
