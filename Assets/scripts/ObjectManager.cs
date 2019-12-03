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
        digitObjects = new List<GameObject>();
        isLastScene = SceneLoader.Instance.isLastScene();

        displayKeypad();

        if (this.isLastScene || SceneLoader.Instance.getSceneId() % 2 == 0) {
            appendDigitObjects();
        }
    }

    void displayKeypad()
    {
        if (!this.isLastScene) { return; }

        buttonA.SetActive(false);
        buttonB.SetActive(false);
        keypad.SetActive(true);
    }

    void appendDigitObjects()
    {
        Debug.Log(SceneLoader.Instance.getSceneId() + " scene ID");

        if(!SceneLoader.Instance.hasDisplayedObject(0))
        {
            digitObjects.Add(pillowA);
            digitObjects.Add(pillowB);
        }

        if (!SceneLoader.Instance.hasDisplayedObject(1))
        {
            digitObjects.Add(notebookA);
            digitObjects.Add(notebookB);
        }

        if (!SceneLoader.Instance.hasDisplayedObject(2))
        {
            digitObjects.Add(boxA);
            digitObjects.Add(boxB);
        }

        if (!SceneLoader.Instance.hasDisplayedObject(3))
        {
            digitObjects.Add(dishA);
            digitObjects.Add(dishB);
        }

        displayDigitObject();
    }

    void displayDigitObject()
    {
        if(digitObjects.Count == 0) { return; }

        int i = UnityEngine.Random.Range(0, digitObjects.Count - 1);
        GameObject digitObject = digitObjects[i];
        string tagName = digitObject.tag;

        digitObject.SetActive(true);
        digitObjects.RemoveAt(i);

        GameObject sibling = digitObjects.Find(obj => obj.tag == tagName);
        digitObjects.RemoveAt(digitObjects.IndexOf(sibling));

        switch (tagName)
        {
            case "pillow":
                SceneLoader.Instance.pushDisplayObject(0);
                break;
            case "notebook":
                SceneLoader.Instance.pushDisplayObject(1);
                break;
            case "box":
                SceneLoader.Instance.pushDisplayObject(2);
                break;
            case "dish":
                SceneLoader.Instance.pushDisplayObject(3);
                break;
            default:
                break;
        }
    }
}
