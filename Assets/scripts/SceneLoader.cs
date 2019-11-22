using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;



public class SceneLoader : MonoBehaviour
{
    List<int> distractionScenes = new List<int> { 0, 1, 2, 3, 4, 5 };
    List<int> noDistractionScenes = new List<int> { 6, 7, 8, 9, 10, 11 };
    int count = 0;

    private void Start()
    {
        Randomizer.Shuffle(noDistractionScenes);
        Randomizer.Shuffle(distractionScenes);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Controller" && count < distractionScenes.Count)
        {
            SceneManager.LoadScene(distractionScenes[count]);
            count++;
        }
    }
}

// Shuffle any (I)List with an extension method based on
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


