using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log(gameObject.name + " was triggered by " + other.gameObject.name);

        if (other.gameObject.tag == "Controller")
        {

            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if (currentScene == 0)
            {
                SceneManager.LoadScene(1);
            }
            
        }
    }
}
