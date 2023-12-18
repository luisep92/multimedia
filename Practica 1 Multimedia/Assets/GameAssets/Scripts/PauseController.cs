using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0.0001f;
        }
    }
}
