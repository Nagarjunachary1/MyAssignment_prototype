using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int mRows, mColums;
    public GridLayoutGroup tilesHolder;

    public GameObject cardPrefab;
    public Sprite[] cardImages;
    public List<Tile> allCardsList;




    private int totalPairs;



    // Start is called before the first frame update
    void Start()
    {


        GenerateTiles();

        CardTilesHandler();
    }

   public void GenerateTiles()
    {




        RectTransform _recTrans = tilesHolder.GetComponent<RectTransform>();

        float _correctionValue = (mColums>6 || mRows > 6) ? 100 : 150;

        _recTrans.sizeDelta = new Vector2(mColums* _correctionValue, mRows* _correctionValue);


        float _parentWidth = _recTrans.rect.width;
        float _parentHeight = _recTrans.rect.height;


        float _cellWidth = (_parentWidth - (tilesHolder.spacing.x * (mColums - 1))) / mColums; 
        float _cellHeight = (_parentHeight - (tilesHolder.spacing.y * (mRows - 1))) / mRows;

        tilesHolder.cellSize = new Vector2(_cellWidth, _cellHeight);



        for (int i = 0; i < mRows * mColums; i++)
        {
            GameObject _cardObj = Instantiate(cardPrefab, tilesHolder.transform);
            allCardsList.Add(_cardObj.GetComponent<Tile>());

        }
    }



    void CardTilesHandler()
    {

        totalPairs = (mRows * mColums) / 2;


        // Creating 2 cards pair values
        List<int> _cardValues = new List<int>();
        for (int i = 0; i < totalPairs ; i++)
        {
            _cardValues.Add(i);
            _cardValues.Add(i);
        }


        Utility.Shuffle(_cardValues);


        for (int i = 0; i < (mRows * mColums); i++)
        {
           // Debug.Log("CARD SHUFFLE " + i + "==>" + _cardValues[i]);
            allCardsList[i].InitCards(cardImages[_cardValues[i]], _cardValues[i]);

        }


 

    }


}
