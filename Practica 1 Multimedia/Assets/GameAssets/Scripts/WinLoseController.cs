using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Luis Escolano Piquer
// Controlador de la escena de ganar o perder

public class WinLoseController : MonoBehaviour
{
    [SerializeField] TMP_Text txtMain;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtBtn;
    [SerializeField] Image background;
  
    // Setup scene
    void Start()
    {
        txtScore.text = "Score: " + GameManager.Instance.Score;
        if (!GameManager.Instance.IsPlayerAlive)
        {
            txtBtn.text = "Restart";
            txtMain.text = "YOU DIED";
        }

        if (GameManager.Instance.CurrentLevel == "MainMenu")
        {
            txtBtn.text = "Main Menu";
            GameManager.Instance.Score = 0;
        }
    }

    // Defines next scene button
    public void MainBtn()
    {
        if (GameManager.Instance.IsPlayerAlive)
        {
            string cl = GameManager.Instance.CurrentLevel;
            if (cl == "Level1" || cl == "Level2")
                SceneManager.LoadScene(GameManager.Instance.CurrentLevel);
            else
                SceneManager.LoadScene("MainMenu");
        }
        else
            SceneManager.LoadScene(GameManager.Instance.CurrentLevel);
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
