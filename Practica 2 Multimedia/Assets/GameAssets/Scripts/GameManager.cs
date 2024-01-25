using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Luis Escolano Piquer

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Health = 3;
    [SerializeField] Image fade;

    // Singleton
    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Fade, load scene
    public void LoadScene(string name)
    {
        StartCoroutine(ChangeScene(name));
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (fade == null)
            return;
        fade.color = Color.black;
        StartCoroutine(FadeOut());
    }

    // Leave scene fading
    private IEnumerator ChangeScene(string name, float duration = 1)
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(name);
    }

    private IEnumerator FadeOut(float duration = 1f)
    {
        Color c = Color.black;
        c.a = fade.color.a - (Time.deltaTime / duration);
        fade.color = c;
        yield return new WaitForEndOfFrame();
        if(fade.color.a >= 0f)
            StartCoroutine(FadeOut(duration));
    }

    private IEnumerator FadeIn(float duration = 1f)
    {
        Color c = Color.black;
        c.a = fade.color.a + (Time.deltaTime / duration);
        fade.color = c;
        yield return new WaitForEndOfFrame();
        if (fade.color.a <= 1f)
            StartCoroutine(FadeIn(duration));
    }
}
