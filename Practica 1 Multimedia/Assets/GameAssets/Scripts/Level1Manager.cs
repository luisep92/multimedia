using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        txtPhase.gameObject.SetActive(false);
        waves = GetWaves();
        StartCoroutine(CheckPhaseEnded());
        StartCoroutine(SpawnMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        
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
            {
                SceneManager.LoadScene("WinLose");
                GameManager.Instance.CurrentLevel = "Level2";
            }
            StartCoroutine(ChangePhase());
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(CheckPhaseEnded());
    }
}
