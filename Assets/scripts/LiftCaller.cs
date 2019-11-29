using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCaller : MonoBehaviour
{
    float requiredTime;

    private void Start()
    {
        requiredTime = Time.time + 7.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Controller") || Time.time < requiredTime) { return; }
     
        SceneLoader.Instance.loadLoadingScreen();
    }
}
