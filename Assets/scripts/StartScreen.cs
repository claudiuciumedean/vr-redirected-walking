using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour
{
    public float timeThreshold;
    public TextMesh text;
    public GameObject messageObject;
    float timeLeft;
    bool enter = false;
    string answer;

    private void Update()
    {
        if(!enter) { return; }

        timeLeft -= Time.deltaTime;
  
        if (timeLeft < 0)
        {
            if (messageObject.tag == "Message") // when in start scene
            {
                SceneLoader.Instance.loadFirstScene();
                return;
            }

            SceneLoader.Instance.setQuestionAnswer(messageObject.tag == "Yes" ? "Yes" : "No"); // pass over the answer to the scene loader, Yes = 1; No = 0
            SceneLoader.Instance.saveLog();
            SceneLoader.Instance.loadScene();
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
