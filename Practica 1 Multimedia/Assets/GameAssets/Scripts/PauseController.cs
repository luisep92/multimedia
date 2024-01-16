using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Luis Escolano Piquer
// Controlador de la pausa

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject btnGodMode;
    private bool isPaused = false;
    private bool playerWasDisabled = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchPause();
    }

    private void SwitchPause()
    {
        if(isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            if (!playerWasDisabled)
                Player.Instance.Enable();
            Time.timeScale = 1f;
        }
        else
        {
            playerWasDisabled = Player.Instance.IsDisabled;
            isPaused = true;
            pausePanel.SetActive(true);
            btnGodMode.SetActive(GameManager.Instance.Difficulty < 3);
            if (!playerWasDisabled)
                Player.Instance.Disable();
            Time.timeScale = 0.0001f;
        }
    }

    public void BtnResume()
    {
        SwitchPause();
    }

    public void BtnMainMenu()
    {
        SwitchPause();
        SceneManager.LoadScene("MainMenu");
    }

    public void BtnRestart()
    {
        SwitchPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BtnGodMode()
    {
        Player player = Player.Instance;
        player.SwitchGodMode();
        Color nextColor = player.GodMode ? new Color(64f / 255f, 1f, 225f / 255f) : Color.white;
        Button button = GameObject.Find("btnGodMode").GetComponent<Button>();
        button.image.color = nextColor;
    }
}
