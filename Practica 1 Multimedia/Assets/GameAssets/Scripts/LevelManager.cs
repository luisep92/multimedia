using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TMP_Text txtPhase;
    [SerializeField] TMP_Text txtScore;
    [SerializeField] GameObject meteorPref;
    [SerializeField] List<GameObject> enemies;
    public List<GameObject[]> waves = new();
    private int wave = 0;
    private float sceneLimit;

    public int Wave => wave;
    

    void Start()
    {
        GameManager.Instance.InitDefaultData();
        txtPhase.gameObject.SetActive(false);
        sceneLimit = GetLimits();
        waves = GetWaves();
        StartCoroutine(CheckPhaseEnded());
        StartCoroutine(SpawnMeteor());
    }



    // Get limits of scene based in background image.
    private float GetLimits()
    {
        return (GameObject.Find("Background_game").GetComponent<SpriteRenderer>().size.x / 2) - 1;
    }

    // Waves of enemies.
    private List<GameObject[]> GetWaves()
    {
        List<GameObject[]> ret = new()
        {
            new GameObject[] { enemies[0], enemies[0], enemies[0] },
            new GameObject[] { enemies[0], enemies[0], enemies[1], enemies[0], enemies[0] },
            new GameObject[] { enemies[1], enemies[0], enemies[1], enemies[0], enemies[1] },
            new GameObject[] { enemies[0], enemies[0], enemies[0], enemies[0], enemies[0], enemies[0], enemies[0], enemies[0] },
            new GameObject[] { enemies[1], enemies[1], enemies[1], enemies[1], enemies[1] },
        };
        return ret;
    }

    // Instantiate enemies.
    private void InstantiateWave()
    {
        GameObject[] currentWave = waves[wave];

        for (int i = 0; i < currentWave.Length; i++)
        {
            float t = i / (float)(currentWave.Length - 1);
            float posX = Mathf.Lerp(-sceneLimit, sceneLimit, t);
            Vector3 pos = new(posX, 3.5f, 0);
            Instantiate(currentWave[i], pos, Quaternion.Euler(0, 0, 180));
        }
    }

    // Plays text animation, then instance wave
    private IEnumerator ChangePhase()
    {
        txtPhase.gameObject.SetActive(true);
        txtPhase.text = "PHASE " + (wave + 1);
        txtPhase.GetComponent<Animator>().Play("txtPhase");
        yield return new WaitForSeconds(3.5f);
        txtPhase.gameObject.SetActive(false);
        InstantiateWave();
        wave++;
    }

    private bool PhaseEnded()
    {
        return GameObject.FindWithTag("Enemy") == null;
    }

    IEnumerator CheckPhaseEnded()
    {
        if (PhaseEnded())
        {
            if (wave >= waves.Count)
                SceneManager.LoadScene("WinLose");
            StartCoroutine(ChangePhase());
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(CheckPhaseEnded());
    }

    IEnumerator SpawnMeteor()
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
}
