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

    public GameObject winParticleObj;


    private float totalPairs;
    private Queue<Tile> flipCardsQueue = new Queue<Tile>();
    private Tile firstCard;
    private Tile secondCard;


    private int currentLevel = 1;
    private int score;
    private int movesCount = 0;

    //Best Score and Moves
    string scoreKey ;
    string movesKey ;

    int bestScore =0;
    int bestMoves =1000;


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


        GetBestScoreData();


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
            movesCount++;

            if (firstCard.name == secondCard.name)
            {
                
                score += 10;
                firstCard.MatchFun();
                secondCard.MatchFun();

                totalPairs--;

               

                StartCoroutine(DelayComplete());
               
            }
            else
            {
                score -= 2;

                Invoke(nameof(RevertCards), 1);
              
            }


            ScoreEvents.OnScoreUpdated(score);
            ScoreEvents.OnMovesUpdated(movesCount);
        }

    }


    void RevertCards()
    {
         firstCard.Revertback();
         secondCard.Revertback();
        AudioEvents.InCorrectmatchSound();

    }


    IEnumerator DelayComplete()
    {
        yield return new WaitForSeconds(0.3f);
        AudioEvents.CorrectMatchSound();


        yield return new WaitForSeconds(0.5f);


        if (totalPairs <= 0)
        {
            AudioEvents.WinSound();
            winParticleObj.SetActive(true);

        }

            yield return new WaitForSeconds(0.5f);


        if (totalPairs <= 0)
        {
           
            if (score > bestScore)
            {
                PlayerPrefs.SetInt(scoreKey, score);
                PlayerPrefs.Save();
                UiManager.instance.LevelComplete(true); // on complete passing best_score bool to set badge active


            }
            else
            {
                UiManager.instance.LevelComplete(false);
            }



            if (movesCount < bestMoves)
                PlayerPrefs.SetInt(movesKey, movesCount); PlayerPrefs.Save();


        }

    }

    ////
    ///

    void GetBestScoreData()
    {
        scoreKey = $"level{currentLevel}_PrefScore";
        movesKey = $"level{currentLevel}_PrefMoves";


        bestScore = PlayerPrefs.GetInt(scoreKey, 0);
        bestMoves = PlayerPrefs.GetInt(movesKey, 1000);
    }




}
