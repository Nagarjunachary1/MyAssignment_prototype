using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{

    public GameObject TitleObj;

    public GameObject[] levelTiles;

    // Start is called before the first frame update
    void Start()
    {

        iTween.MoveFrom(TitleObj.gameObject,
           iTween.Hash("y", TitleObj.transform.position.y+100));

        for (int i=0;i<5;i++)
        {
            iTween.ScaleFrom(levelTiles[i],
                iTween.Hash("x", 0, "time", 0.5, "delay", (0.5f+(i*0.2f)),
                "easetype", iTween.EaseType.easeOutElastic));
        }
    }

    public async void LoadGame(int _lvlNum)
    {
        AudioEvents.ButtonClickSound();

        Utility.levelNumber = _lvlNum;

        await Task.Delay(400);

        SceneManager.LoadSceneAsync("Game");
    }
}
