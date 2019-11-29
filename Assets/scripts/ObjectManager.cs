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
        isLastScene = SceneLoader.Instance.isLastScene();

        displayKeypad();

        Debug.Log(SceneLoader.Instance.getSceneId() + "sceneid");
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
        if (SceneLoader.Instance.digitObjects == null) { return; }

        if (!SceneLoader.Instance.getDigitObject("pillow"))
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

        displayDigitObject();
    }

    void displayDigitObject()
    {
        if(digitObjects.Count == 0) { return; }

        int i = UnityEngine.Random.Range(0, digitObjects.Count - 1);
        Debug.Log(digitObjects.Count + "arr");
        GameObject digitObject = digitObjects[i];
        string tagName = digitObject.tag;

        digitObject.SetActive(true);
        digitObjects.RemoveAt(i);

        GameObject sibling = digitObjects.Find(obj => obj.tag == tagName);

        digitObjects.RemoveAt(digitObjects.IndexOf(sibling));
        SceneLoader.Instance.setDigitObject(tagName, true);
    }
}
