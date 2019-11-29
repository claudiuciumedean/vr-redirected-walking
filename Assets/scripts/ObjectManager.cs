using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject keypad;
    public GameObject buttonB;
    public GameObject buttonA;

    public GameObject pillowA;
    public GameObject pillowB;
    public GameObject notebookA;
    public GameObject notebookB;
    public GameObject boxA;
    public GameObject boxB;
    public GameObject dishA;
    public GameObject dishB;

    List<GameObject> digitObjects = new List<GameObject>();
    bool isLastScene;

    void Start()
    {

        if(!SceneLoader.Instance.getDigitObject("pillow"))
        {
            digitObjects.Add(pillowA);
            digitObjects.Add(pillowB);
        }

        if (!SceneLoader.Instance.getDigitObject("notebook"))
        {
            digitObjects.Add(notebookA);
            digitObjects.Add(notebookB);
        }
 
        if (!SceneLoader.Instance.getDigitObject("box"))
        {
            digitObjects.Add(boxA);
            digitObjects.Add(boxB);
        }

        if (!SceneLoader.Instance.getDigitObject("dish"))
        {
            digitObjects.Add(dishA);
            digitObjects.Add(dishB);
        }

        isLastScene = SceneLoader.Instance.isLastScene();

        displayKeypad();
        displayDigitObject();
    }

    void displayKeypad()
    {
        if (!this.isLastScene) { return; }

        buttonA.SetActive(false);
        buttonB.SetActive(false);
        keypad.SetActive(true);
    }

    void displayDigitObject()
    {
        if(digitObjects.Count == 0) { return; }

        int i = UnityEngine.Random.Range(0, digitObjects.Count - 1);
        GameObject digitObject = digitObjects[i];
        string tagName = digitObject.tag;
        GameObject sibling = digitObjects.Find(obj => obj.tag == tagName);

        digitObject.SetActive(true);
        digitObjects.RemoveAt(i);
        digitObjects.RemoveAt(digitObjects.IndexOf(sibling));
        SceneLoader.Instance.setDigitObject(tagName, true);
    }
}
