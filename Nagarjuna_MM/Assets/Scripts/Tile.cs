using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public GameObject hide_obj;
    public Image main_obj;

    [SerializeField]
    private Button this_btn;
    public event Action<Tile> OnCardFlipped;

    public static bool canClick=true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    public void InitCards(Sprite _cardImage, int _tileIndex)
    {
       // Debug.Log("INIT cards --> "+this.name);
        main_obj.sprite = _cardImage;
        this.name = "t" + _tileIndex;

    }

    public void ButtonClick()
    {

        canClick = false;
        iTween.ScaleFrom(this.gameObject,
            iTween.Hash("x",0.8f,"y",0.5f)) ;


        iTween.RotateFrom(this.gameObject,
            iTween.Hash("y",180,"time",0.5f,
            "oncomplete", nameof(CompleteFun),
            "oncompletetarget", gameObject));

         

    }

    void CompleteFun()
    {
        hide_obj.SetActive(false);

        main_obj.gameObject.SetActive(true);

        OnCardFlipped(this);

        canClick = true;
        //  Invoke("Mdelay",1);

    }



    void CompleteFunRevert()
    {
        hide_obj.SetActive(true);
        main_obj.gameObject.SetActive(false);


    }



    public void MatchFun()
    {
        this_btn.interactable = false;
    }

    public void Revertback()
    {

        hide_obj.SetActive(true);
        main_obj.gameObject.SetActive(false);

        iTween.RotateFrom(this.gameObject,
            iTween.Hash("y", 180, "time", 0.5f,
            "oncomplete", nameof(CompleteFunRevert),
            "oncompletetarget", gameObject));

       
    }
}
