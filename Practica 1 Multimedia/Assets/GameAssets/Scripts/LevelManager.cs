using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class LevelManager : MonoBehaviour
{
    protected float sceneLimit;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] GameObject meteorPref;
    

    protected virtual void Start()
    {
        GameManager.Instance.InitDefaultData();
        sceneLimit = GetLimits();;
    }

    // Get limits of scene based in background image.
    protected float GetLimits()
    {
        return (GameObject.Find("Background_game").GetComponent<SpriteRenderer>().size.x / 2) - 1;
    }

    protected IEnumerator SpawnMeteor()
    {
        float t = Random.Range(3, 10);
        yield return new WaitForSeconds(t);
        Instantiate(meteorPref, Vector3.zero, Quaternion.identity);
        StartCoroutine(SpawnMeteor());
    }

    public void Notify()
    {
        txtScore.text = "Score: " + GameManager.Instance.Score;
    }

    protected void StartMusic()
    {
        GetComponent<AudioSource>().Play();
    }
}
