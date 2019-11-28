using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour
{
    public float timeThreshold;
    public TextMesh text;
    public GameObject thisObject;
    float timeLeft;
    bool enter = false;
    string answer;
    
    void Start(){
        //Debug.Log(PersistenceMangager.Instance.id);
        //Debug.Log(PersistenceMangager.Instance.overlap);
        //Debug.Log(PersistenceMangager.Instance.isDistraction);
        
    }

    private void Update()
    {   
        SceneManager.LoadScene(0);
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
                    log(PersistenceMangager.Instance.id, PersistenceMangager.Instance.overlap, PersistenceMangager.Instance.isDistraction, answer);
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


    void log(int id, string overlapLevel, string distractor, string possible) {
        CSVManager.AppendToReport(new string[] { id.ToString(), overlapLevel, distractor, possible });
    }


}
