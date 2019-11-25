using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCaller : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Controller")) { return; }
        SceneLoader.Instance.loadScene();
    }
}
