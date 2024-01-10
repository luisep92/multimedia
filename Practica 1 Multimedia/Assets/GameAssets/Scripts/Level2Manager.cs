using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : LevelManager
{
    public List<EnemyBoss> bosses;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Player.Instance.Disable();
        StartCoroutine(StartAnimation());
        StartCoroutine(CheckPhaseEnded());

    }

    // Start begin animation
    private IEnumerator StartAnimation()
    {
        Player.Instance.Disable();
        AudioSource aSource = GetComponent<AudioSource>();
        aSource.Stop();
        yield return new WaitForSeconds(6f);
        StartMusic();
        Player.Instance.Enable();
        FindObjectOfType<Parallax>().Speed = 1;
    }


    protected override bool PhaseEnded()
    {
        return bosses.Count <= 0;
    }

    // Check if phase has ended every 5 seconds
    protected IEnumerator CheckPhaseEnded(float cooldown = 5)
    {
        if (PhaseEnded())
        {
            GameManager.Instance.CurrentLevel = "MainMenu";
            SceneManager.LoadScene("WinLose");
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(CheckPhaseEnded());
    }
}
