using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsPlayerAlive { get; set; }
    public string CurrentLevel { get; set; }
    private LevelManager levelManager;
    private int score;

    public int Score 
    {
        get => score;
        set
        {
            score = value;
            if (levelManager != null)
                levelManager.Notify();
        }
    }


    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

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

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetWave()
    {
        return levelManager.Wave;
    }
}
