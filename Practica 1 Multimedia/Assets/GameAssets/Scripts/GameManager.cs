using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{
    [Range(1, 3)] public int Difficulty = 1;
    public static GameManager Instance;
    private LevelManager levelManager;
    private string _currentLevel = "Level1";
    private int _score;
    private bool _isPlayerAlive = true;

    private LevelManager LevelManager => levelManager;

    public bool IsPlayerAlive 
    {
        get => _isPlayerAlive;
        set => _isPlayerAlive = value;
    }

    public string CurrentLevel { 
        get => _currentLevel;
        set => _currentLevel = value;
    }

    public int Score 
    {
        get => _score;
        set
        {
            _score = value;
            if (levelManager != null)
                levelManager.Notify();
        }
    }


    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Default state
    public void InitDefaultData()
    {
        IsPlayerAlive = true;
        CurrentLevel = SceneManager.GetActiveScene().name;
    }

    // Restart last level
    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Get active wave of enemies
    public int GetWave()
    {
        var lm = (Level1Manager)levelManager;
        return lm.Wave;
    }
}
