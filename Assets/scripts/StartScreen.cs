using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartScreen : MonoBehaviour
{
    public float timeThreshold;
    public TextMesh text;
    public GameObject thisObject;
    float timeLeft;
    bool enter = false;
    string answer;

    string id;
    string overlap;
    string isDistraction;

    
    void Start(){

    }

    private void Update()
    {   Debug.Log(id + " "  + overlap + " " + isDistraction);

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
                    
                    answer = thisObject.tag == "Yes" ? "Yes" : "No"; // pass over the answer to the log, Yes = 1; No = 0
                    //log(thisId,);
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

    public static void sendData(StartScreen start, string id, string overlap, string isDistraction){
        start.id = id;
        start.overlap = overlap;
        start.isDistraction = isDistraction;
        }

    void log(int id, string overlapLevel, string distractor, string possible) {
        CSVManager.AppendToReport(new string[] { id.ToString(), overlapLevel, distractor, possible });
    }


}
