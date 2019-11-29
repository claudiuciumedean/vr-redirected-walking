using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shuffle any list with an extension method based on
// the Fisher-Yates shuffle https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
public static class Randomizer
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
