using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneLoader : Singleton<SceneLoader>
{
    public int sceneId;

    List<int> distractionScenes = new List<int> { 0, 1, 2, 3, 4, 5 };
    List<int> noDistractionScenes = new List<int> { 6, 7, 8, 9, 10, 11 };
    bool isRandomized = false;
    int count;
    public int playerId = 0;
    string overlapLevel;
    string questionAnswer;
    bool hasDistraction = false;
    bool hasStartedFirstTrial = false;
    List<int> displayedObjects = new List<int>();
    System.Random randomNumber = new System.Random();

    private void Start()
    {
        if (!isRandomized)
        {
            Randomizer.Shuffle(noDistractionScenes);
            Randomizer.Shuffle(distractionScenes);
            isRandomized = true;
        }

        CSVManager.setFileName(playerId);
    }

    public void loadLoadingScreen()
    {
        SceneManager.LoadScene(13); //scene 13 is Load scene
    }

    public void loadFirstScene()
    {
        count = 0;
        if (!hasStartedFirstTrial)
        {
            hasDistraction = randomNumber.Next(1, 10) > 5 ? true : false;
            hasStartedFirstTrial = true;
        }
        else
        {
            hasDistraction = hasDistraction ? false : true;
            displayedObjects = new List<int>();
        }

        SceneManager.LoadScene(this.hasDistraction ? distractionScenes[count] : noDistractionScenes[count]);
        sceneId = count;
        count++;
    }

    public void loadScene()
    {
        if (count == distractionScenes.Count) {
            SceneManager.LoadScene(12); //scene 12 is start scene
            return;
        }

        if (this.hasDistraction)
        {
            SceneManager.LoadScene(distractionScenes[count]);
        }
        else
        {
            SceneManager.LoadScene(noDistractionScenes[count]);
        }

        sceneId = count;
        count++;
    }

    public bool isLastScene()
    {
        return count == distractionScenes.Count;
    }

    public int getSceneId()
    {
        return sceneId;
    }

    public void setQuestionAnswer(string answer)
    {
        questionAnswer = answer;
    }

    public void setOverlapLevel(string overlapLevel)
    {
        this.overlapLevel = overlapLevel;
    }

    public void pushDisplayObject(int itm)
    {
        displayedObjects.Add(itm);
    }

    public bool hasDisplayedObject(int itm)
    {
       return displayedObjects.Contains(itm);
    }

    public void saveLog()
    {
        string distractor = this.hasDistraction ? "Yes" : "No";
        CSVManager.AppendToReport(new string[] { this.playerId.ToString(), this.overlapLevel, distractor, this.questionAnswer });
    }
}