using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public abstract class LevelManager : MonoBehaviour
{
    [SerializeField] TMP_Text txtScore;
    [SerializeField] GameObject meteorPref;
    protected float sceneLimit;

    // Define how the phase end
    protected abstract bool PhaseEnded();

    protected virtual void Start()
    {
        GameManager.Instance.InitDefaultData();
        sceneLimit = GetLimits();;
        txtScore.text = GameManager.Instance.Score.ToString();
    }

    // Get limits of scene based in background image.
    protected float GetLimits()
    {
        Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));
        return (GameObject.Find("Background_game").GetComponent<SpriteRenderer>().size.x / 2) - 1;
    }

    // Spawn a meteor between 3 and 10 seconds
    protected IEnumerator SpawnMeteor()
    {
        float t = Random.Range(3, 10);
        yield return new WaitForSeconds(t);
        Instantiate(meteorPref, Vector3.zero, Quaternion.identity);
        StartCoroutine(SpawnMeteor());
    }

    // Update score
    public void Notify()
    {
        txtScore.text = "Score: " + GameManager.Instance.Score;
    }

    // Start background music
    protected void StartMusic()
    {
        var aSource = GetComponent<AudioSource>();
        if(aSource != null)
            aSource.Play();
    }
}
