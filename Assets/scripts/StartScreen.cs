using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartScreen : MonoBehaviour
{
    public float timeThreshold;
    public TextMesh text;
    public GameObject thisObject;
    float timeLeft;
    bool enter = false;
    int answer;

    private String myPath;

    
    void Start(){
        myPath = Application.persistentDataPath + "/testFile.txt";
    }

    private void Update()
    {   
        // on controller enter - start countdown
        if(enter)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {

                if (thisObject.tag == "Message") // when in start scene
                {
                    SceneLoader.Instance.setDistractions(true);
                    SceneLoader.Instance.loadFirstScene();
                }

                else // when not in start scene
                {
                    
                    answer = thisObject.tag == "Yes" ? 1 : 0; // pass over the answer to the log, Yes = 1; No = 0
                    Debug.Log(answer);
                    // go to next scene from here
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        timeLeft = timeThreshold;
        enter = true;
        text.color = Color.red;
        
    }

    private void OnTriggerExit(Collider other)
    {
        enter = false;
        text.color = Color.white;
    }

}
