using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    private bool isPaused = false;


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
            Player.Instance.Enable();
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pausePanel.SetActive(true);
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
