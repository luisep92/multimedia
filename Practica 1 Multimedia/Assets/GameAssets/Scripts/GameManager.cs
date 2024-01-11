using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

// Luis Escolano Piquer
// Orquestador del juego

public class GameManager : MonoBehaviour
{
    [Range(1, 3)] public int Difficulty = 1;
    [SerializeField] Texture2D cursorTexture;
    public static GameManager Instance;
    private LevelManager levelManager;
    private string _currentLevel = "Level1";
    private int _score;
    private bool _isPlayerAlive = true;
    private bool _playerAim = true;
    private LevelManager LevelManager => levelManager;

    public bool PlayerAim
    {
        get => _playerAim;
        set => _playerAim = value;
    }

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
        SetCursor();
    }

    // Get active wave of enemies
    public int GetWave()
    {
        var lm = (Level1Manager)levelManager;
        return lm.Wave;
    }

    private void SetCursor()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Texture2D texture =  IsPlayScene(currentScene) ? cursorTexture : null;
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

    public static bool IsPlayScene(string sceneName)
    {
        return sceneName.Substring(0, 5) == "Level";
    }
}
