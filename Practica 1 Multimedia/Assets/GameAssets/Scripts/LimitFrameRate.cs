using UnityEngine;

// Luis Escolano Piquer
// Si no limitamos el framerate, el juego consume todo lo que puede sin necesidad

public class LimitFrameRate : MonoBehaviour
{
    // Limit framerate, so the game does not get all system resources
    void Start()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 144;
    }
}