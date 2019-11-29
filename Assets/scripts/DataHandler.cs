using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance {get; set; }
    public int id;
    public string overlap;
    public string isDistraction;

    public List<int> distractionScenes;
    public List<int> noDistractionScenes;

    public bool isRandomized = false;
    public int listNumber;

    public int sceneNumber;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else{

            Destroy(gameObject);
        }
    }

}
