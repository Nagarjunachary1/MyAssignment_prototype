using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float mRows, mColums;
    public GridLayoutGroup tilesHolder;

    public GameObject cardPrefab;
    public Sprite[] cardImages;
    public List<Tile> allCardsList;




    private float totalPairs;
    private Queue<Tile> flipCardsQueue = new Queue<Tile>();
    private Tile firstCard;
    private Tile secondCard;


    private int currentLevel = 1;
    private int score;


    private void OnDisable()
    {
        for (int i = 0; i < allCardsList.Count; i++)
        {
            allCardsList[i].OnCardFlipped -= HandleCardFlipped;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        AssignLevel();

        GenerateTiles();

        CardTilesHandler();
    }


    private void AssignLevel()
    {
        currentLevel = Utility.levelNumber;
        mRows = Utility.levelGrids[currentLevel - 1].x;
        mColums = Utility.levelGrids[currentLevel - 1].y;
    }
     
    private void GenerateTiles()
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

            Tile _cardTile =_cardObj.GetComponent<Tile>();
            _cardTile.OnCardFlipped += HandleCardFlipped;

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


        //randomizing the values to fetch images
        Utility.Shuffle(_cardValues);


        for (int i = 0; i < (mRows * mColums); i++)
        {
           // Debug.Log("CARD SHUFFLE " + i + "==>" + _cardValues[i]);
            allCardsList[i].InitCards(cardImages[_cardValues[i]], _cardValues[i]);

        }

    }


    void HandleCardFlipped(Tile _cardTile)
    {
        flipCardsQueue.Enqueue(_cardTile);

        if (flipCardsQueue.Count == 2)
        {

            firstCard = flipCardsQueue.Dequeue();
            secondCard = flipCardsQueue.Dequeue();

            if (firstCard.name == secondCard.name)
            {
                
                score += 10;
                firstCard.MatchFun();
                secondCard.MatchFun();

                totalPairs--;


                if (totalPairs<=0)
                {
                    Invoke(nameof(DelayComplete),1);

                }
            }
            else
            {
                score -= 2;

                Invoke(nameof(RevertCards), 1);
              
            }
        }

    }


    void RevertCards()
    {
         firstCard.Revertback();
         secondCard.Revertback();
    }


    void DelayComplete()
    {
        UiManager.instance.LevelComplete();
    }

   


}
