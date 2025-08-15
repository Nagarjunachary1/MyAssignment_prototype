using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject LC_obj,bestSticker;

    public TMP_Text[] scoreTxt, movesTxt;
    void OnEnable()
    {
        ScoreEvents.OnScoreUpdated += ScoreFun;
        ScoreEvents.OnMovesUpdated += MovesFun;

    }

    void OnDisable()
    {
        ScoreEvents.OnScoreUpdated -= ScoreFun;
        ScoreEvents.OnMovesUpdated -= MovesFun;

    }


    void Awake()
    {
        instance = this;
    }



    void ScoreFun(int num)
    {
        scoreTxt[0].text = $"Score : {num}";
        scoreTxt[1].text = $"Score : {num}";
    }
    void MovesFun(int num)
    {
        movesTxt[0].text = $"Moves : {num}";
        movesTxt[1].text = $"Moves : {num}";
    }


    public void LevelComplete(bool _bestScore = false)
    {
        LC_obj.SetActive(true);

        if (_bestScore)
        {
            bestSticker.SetActive(true);
        }
    }


    public async void LoadNewScene(string _sceneName)
    {
        AudioEvents.ButtonClickSound();

        await Task.Delay(400);
        SceneManager.LoadSceneAsync(_sceneName);
    }



}
