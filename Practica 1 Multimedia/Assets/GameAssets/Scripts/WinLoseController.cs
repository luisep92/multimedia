using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseController : MonoBehaviour
{
    [SerializeField] TMP_Text txtMain;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtBtn;
    [SerializeField] Image background;
  
    void Start()
    {
        txtScore.text = "Score: " + GameManager.Instance.Score;
        if (GameManager.Instance.IsPlayerAlive)
            return;
        background. color = new Color(1f, 0.35f, 0.35f, 1);
        txtBtn.text = "Restart";
        txtMain.text = "YOU DIED";
    }

    public void MainBtn()
    {
        // TODO: Change main menu for next scene
        if (GameManager.Instance.IsPlayerAlive)
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene(GameManager.Instance.CurrentLevel);
    }
}
