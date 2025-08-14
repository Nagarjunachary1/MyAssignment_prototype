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

 
}

