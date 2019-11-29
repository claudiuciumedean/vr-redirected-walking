using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneLoader : Singleton<SceneLoader>
{
    List<int> distractionScenes = new List<int> { 0, 1, 2, 3, 4, 5 };
    List<int> noDistractionScenes = new List<int> { 6, 7, 8, 9, 10, 11 };
    bool isRandomized = false;
    int count;
    public int playerId = 0;
    string overlapLevel;
    string questionAnswer;
    bool hasDistraction = false;
    bool hasStartedFirstTrial = false;
    public Hashtable digitObjects = new Hashtable();
    public int sceneId;


    private void Start()
    {
        if (!isRandomized)
        {
            Randomizer.Shuffle(noDistractionScenes);
            Randomizer.Shuffle(distractionScenes);
            isRandomized = true;
        }

        CSVManager.setFileName(playerId);
        appendDigitObject();
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
            hasDistraction = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
            hasStartedFirstTrial = true;
        }
        else
        {
            hasDistraction = hasDistraction ? false : true;
            // reset the digit object hashtable to false
            // for the second part of the experiment
            setDigitObject("pillow", false);
            setDigitObject("dish", false);
            setDigitObject("box", false);
            setDigitObject("notebook", false);
        }

        SceneManager.LoadScene(this.hasDistraction ? distractionScenes[count] : noDistractionScenes[count]);
        sceneId = distractionScenes[count];
        count++;
    }

    public void loadScene()
    {
        Debug.Log(count + "count");
        Debug.Log(hasDistraction + "hasDistraction");

        if (count == distractionScenes.Count - 1) {
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

        sceneId = distractionScenes[count];
        count++;
    }

    public bool isLastScene()
    {
        return count === distractionScenes.Count - 1;
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
        Debug.Log(this.overlapLevel);
    }

    public void appendDigitObject()
    {
        digitObjects.Add("pillow", false);
        digitObjects.Add("dish", false);
        digitObjects.Add("box", false);
        digitObjects.Add("notebook", false);
    }

    public void setDigitObject(string key, bool isDisplayed) {
        digitObjects[key] = isDisplayed;
    }

    public bool getDigitObject(string key)
    {
        string value = digitObjects["pillow"].ToString();
        return value == "True" ? true : false;
    }

    public void saveLog()
    {
        string distractor = this.hasDistraction ? "Yes" : "No";
        CSVManager.AppendToReport(new string[] { this.playerId.ToString(), this.overlapLevel, distractor, this.questionAnswer });
    }
}