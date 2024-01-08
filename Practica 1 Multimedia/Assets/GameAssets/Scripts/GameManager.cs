using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsPlayerAlive { get; set; }
    public string CurrentLevel { get; set; }
    private LevelManager levelManager;
    private int _score;

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
        // Add OnSceneLoaded() to sceneLoaded event
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
        var lm = (Level1Manager)levelManager;
        return lm.Wave;
    }
}
