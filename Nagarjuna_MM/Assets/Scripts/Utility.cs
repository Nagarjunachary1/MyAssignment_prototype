using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

    // Shuffle logic from the Generic list data
    public static void Shuffle<T>(List<T> list)
    {
       
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


    public static int levelNumber = 1;

    public static Vector2[] levelGrids = new Vector2[]{
       new Vector2(2, 2),
       new Vector2(2, 3),
       new Vector2(4, 5),
       new Vector2(5, 6),
       new Vector2(6, 6)
    };

 
}


public static class ScoreEvents
{
    public static Action<int> OnScoreUpdated;
    public static Action<int> OnMovesUpdated;
}

