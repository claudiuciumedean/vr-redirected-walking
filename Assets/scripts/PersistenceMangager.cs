using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceMangager : MonoBehaviour
{

    public static PersistenceMangager Instance {get; set; }
    public int id;
    public string overlap;
    public string isDistraction;

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
