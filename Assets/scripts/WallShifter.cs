using UnityEngine;

public class WallShifter : MonoBehaviour
{
    public GameObject camera;
    public GameObject wall;
    public GameObject coverWallA;
    public GameObject coverWallB;
    public GameObject cellA;
    public GameObject cellB;

    Animation animatedCellA;
    Animation animatedCellB;

    float playerPositionX;
    float playerPositionZ;
    bool isDoorOpen = false;

    void Start()
    {
        SceneLoader.Instance.setOverlapLevel(wall.tag);

        animatedCellA = cellA.GetComponent<Animation>();
        animatedCellB = cellB.GetComponent<Animation>();
        animatedCellA.Play("Open");
    }

    void Update()
    {
        playerPositionX = camera.transform.position.x;
        playerPositionZ = camera.transform.position.z;
        shiftWall();
    }

    void shiftWall()
    {
        if (playerPositionZ > -0.7) { return; }

        float overlappingGain = 0;
        float currentY = wall.transform.localPosition.y;
        float currentZ = wall.transform.localPosition.z;
        Vector3 temp = Vector3.zero;
        bool toggle = false;

        switch (wall.tag)
        {
            case "overlap-0":
                overlappingGain = 0;
                break;
            case "overlap-15":
                overlappingGain = 0.3f;
                break;
            case "overlap-30":
                overlappingGain = 0.6f;
                break;
            case "overlap-45":
                overlappingGain = 0.9f;
                break;
            case "overlap-60":
                overlappingGain = 1.2f;
                break;
            case "overlap-75":
                overlappingGain = 1.5f;
                break;
            default:
                break;
        }

        if (playerPositionX > 0)
        {
            temp = new Vector3(-overlappingGain, currentY, currentZ);
            toggle = true;

            if (!isDoorOpen)
            {
                isDoorOpen = true;
                animatedCellB.Play("Open");
                animatedCellA.Play("Close");
            }
        }
        else
        {
            temp = new Vector3(overlappingGain, currentY, currentZ);
            toggle = false;

            if (isDoorOpen)
            {
                isDoorOpen = false;
                animatedCellA.Play("Open");
                animatedCellB.Play("Close");
            }
        }

        if (coverWallA != null && coverWallB != null)
        {
            coverWallA.SetActive(toggle);
            coverWallB.SetActive(!toggle);
        }

        wall.transform.localPosition = temp;
    }
}