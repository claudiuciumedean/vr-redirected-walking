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
    bool switchBlock = false;
    
    

    
    void Start(){
        
        if(!DataHandler.Instance.isRandomized){

        List<int> distractionScenes = new List<int> { 0, 1, 2, 3, 4, 5 };
        List<int> noDistractionScenes = new List<int> { 6, 7, 8, 9, 10, 11 };
        Randomizer.Shuffle(distractionScenes);
        Randomizer.Shuffle(noDistractionScenes);
        DataHandler.Instance.distractionScenes = distractionScenes;
        DataHandler.Instance.noDistractionScenes = noDistractionScenes;
        DataHandler.Instance.isRandomized = true;
        DataHandler.Instance.listNumber = UnityEngine.Random.Range(0,1);

        }

        else{
            if (DataHandler.Instance.sceneNumber > 5){
                DataHandler.Instance.sceneNumber = 0;
                DataHandler.Instance.listNumber = DataHandler.Instance.listNumber == 1 ? 0 : 1;
                switchBlock = true;
                
            }
        }
        
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
                    if(DataHandler.Instance.listNumber == 1 ) {
                        SceneManager.LoadScene(DataHandler.Instance.distractionScenes[DataHandler.Instance.sceneNumber]);
                        }

                    else {
                        SceneManager.LoadScene(DataHandler.Instance.noDistractionScenes[DataHandler.Instance.sceneNumber]);
                        }
                }

                else // when not in start scene
                {
                    
                    answer = thisObject.tag == "Yes" ? "Yes" : "No"; // pass over the answer to the log, Yes = 1; No = 0
                    log(DataHandler.Instance.id, DataHandler.Instance.overlap, DataHandler.Instance.isDistraction, answer);

                    DataHandler.Instance.sceneNumber++;

                    if(switchBlock) {
                        SceneManager.LoadScene(12);
                        }

                    else if(DataHandler.Instance.listNumber == 1) {
                        SceneManager.LoadScene(DataHandler.Instance.distractionScenes[DataHandler.Instance.sceneNumber]);}


                    else {
                        SceneManager.LoadScene(DataHandler.Instance.noDistractionScenes[DataHandler.Instance.sceneNumber]);
                        }
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
