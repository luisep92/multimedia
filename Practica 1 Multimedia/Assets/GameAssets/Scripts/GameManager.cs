using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Score { get; set; }
    public bool IsPlayerAlive { get; set; }
    public string CurrentLevel { get; set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void InitDefaultData()
    {
        Score = 0;
        IsPlayerAlive = true;
        CurrentLevel = SceneManager.GetActiveScene().name;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }
}
