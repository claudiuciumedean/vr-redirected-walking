using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneLoader : Singleton<SceneLoader>
{
    public GameObject player;
    List<int> distractionScenes = new List<int> { 0, 1, 2, 3, 4, 5 };
    List<int> noDistractionScenes = new List<int> { 6, 7, 8, 9, 10, 11 };
    int count = 0;
    bool displayDistractions = false;
    bool isRandomized = false;

    private void Start()
    {
        if (!isRandomized)
        {
            Randomizer.Shuffle(noDistractionScenes);
            Randomizer.Shuffle(distractionScenes);
            isRandomized = true;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(player);
    }

    public void setDistractions(bool displayDistractions)
    {
        this.displayDistractions = displayDistractions;
    }

    public void loadFirstScene()
    {
        if (this.displayDistractions)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(noDistractionScenes[count]);
        }

        count++;
    }

    public void loadScene()
    {
        //if (count >= distractionScenes.Count) { return; }
        Debug.Log("asfa");
        //SteamVR_LoadLevel tempload = player.GetComponent<SteamVR_LoadLevel>();
        //tempload.fadeOutTime = 1f;
        //tempload.fadeInTime = 1f;
        //tempload.Trigger();

        //SceneManager.LoadScene(distractionScenes[count]);
        //count++;
    }
}

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


