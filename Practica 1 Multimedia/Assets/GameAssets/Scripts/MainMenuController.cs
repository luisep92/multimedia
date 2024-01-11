using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Luis Escolano Piquer
// Controlador del menu principal

public class MainMenuController : MonoBehaviour
{
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ToggleAim()
    {
        GameManager.Instance.PlayerAim = FindObjectOfType<Toggle>().isOn;
    }
}
