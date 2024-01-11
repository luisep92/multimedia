using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// Luis Escolano Piquer
// Encargado de gestionar el nivel 1

public class Level1Manager : LevelManager
{
    [SerializeField] TMP_Text txtPhase;
    [SerializeField] List<GameObject> enemies;
    public List<GameObject[]> waves = new();
    private int wave = 0;

    public int Wave => wave;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        GameManager.Instance.Score = 0;
        txtPhase.gameObject.SetActive(false);
        waves = GetWaves();
        StartCoroutine(CheckPhaseEnded());
        StartCoroutine(SpawnMeteor());
    }

   
    // Waves of enemies.
    // 0 = Shooter
    // 1 = Kamikaze
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

    // Instantiate wave. 
    private void InstantiateWave()
    {
        GameObject[] currentWave = waves[wave];

        for (int i = 0; i < currentWave.Length; i++)
        {
            float interpolator = i / (float)(currentWave.Length - 1);
            float posX = Mathf.Lerp(-sceneLimit, sceneLimit, interpolator);
            Vector3 pos = new(posX, 3.5f, 0);
            Instantiate(currentWave[i], pos, Quaternion.Euler(0, 0, 180));
        }
    }

    // Plays text animation, then instance wave
    private IEnumerator ChangePhase(float animTime = 3.5f)
    {
        txtPhase.gameObject.SetActive(true);
        txtPhase.text = "PHASE " + (wave + 1);
        txtPhase.GetComponent<Animator>().Play("txtPhase");
        yield return new WaitForSeconds(animTime);
        txtPhase.gameObject.SetActive(false);
        InstantiateWave();
        wave++;
    }

    protected override bool PhaseEnded()
    {
        return GameObject.FindWithTag("Enemy") == null;
    }

    // Check if phase ended ecery 5 seconds
    IEnumerator CheckPhaseEnded(float cooldown = 5f)
    {
        if (PhaseEnded())
        {
            if (wave >= waves.Count)
            {
                GameManager.Instance.CurrentLevel = "Level2";
                SceneManager.LoadScene("WinLose");
            }
            StartCoroutine(ChangePhase());
        }
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(CheckPhaseEnded());
    }


}
