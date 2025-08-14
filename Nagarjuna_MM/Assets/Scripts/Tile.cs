using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public GameObject hide_obj;
    public Image main_obj;
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

        iTween.ScaleFrom(this.gameObject,
            iTween.Hash("x",0.8f,"y",0.5f)) ;


        iTween.RotateFrom(this.gameObject,
            iTween.Hash("y",180,"time",0.5f,
            "oncomplete", "CompleteFun",
            "oncompletetarget", gameObject));

    }

    void CompleteFun()
    {
        hide_obj.SetActive(false);

        main_obj.gameObject.SetActive(true);

     //  Invoke("Mdelay",1);
    }

    void Mdelay()
    {
        hide_obj.SetActive(true);
        main_obj.gameObject.SetActive(false);


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
