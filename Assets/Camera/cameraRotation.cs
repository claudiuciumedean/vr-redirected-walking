using UnityEngine;

// Camera rotation script
public class cameraRotation : MonoBehaviour
{
    float delay = 5.0f; // delay before to avoid rotation offset
    float animLength = 20.0f; // length of animation in seconds
    float phase;
    float maxAngle = 90.0f;
    float timeStarted;
    float timeElapsed;

    AudioSource cameraSound;
    public bool play = false;

    void Start()
    {
        cameraSound = GetComponent<AudioSource>();
        timeStarted = Time.time;
    }

    void Update()
    {
        // Calculate the time elapsed since the scene has started
        timeElapsed = Time.time - timeStarted;

        // Calculate the phase of the animation
        phase = (timeElapsed - delay) % animLength / animLength;

        // Wait 5 sec after the game starts to avoid offset
        if (timeElapsed < delay)
        {
            phase = 0.0f;
        }

        float rotSpeed = maxAngle * Time.deltaTime / (animLength * 0.05f);

        // Rotate camera according to the animation phase
        // Start from the center, turn left first
        
        if (phase > 0.0f && phase < 0.05f)
        {
            play = true;
            transform.Rotate(0.0f, rotSpeed, 0.0f, Space.World);

        }
        else if (phase > 0.5f && phase < 0.6f)
        {

            play = true;
            transform.Rotate(0.0f, -rotSpeed, 0.0f, Space.World);

        }
        else if (phase > 0.95f && phase < 1.0f)
        {

            play = true;
            transform.Rotate(0.0f, rotSpeed, 0.0f, Space.World);

        }

        soundManager(play, cameraSound.isPlaying);
    }

    void soundManager(bool check, bool isPlaying)
    {
        if (check == true && isPlaying == false)
        {
            cameraSound.Play(0);
            play = false;
        }
        else if (check == true && isPlaying == true)
        {
            // do nothing
        }
        else
        {
            cameraSound.Stop();
            play = false;
        }
    }
}