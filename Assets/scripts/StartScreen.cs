using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    float timeLeft = 10.0f;


    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            SceneLoader.Instance.setDistractions(true);
            SceneLoader.Instance.loadFirstScene();
        }
    }

}
