using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : LevelManager
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartMusicCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartMusicCoroutine()
    {
        yield return new WaitForSeconds(6f);
        StartMusic();
    }
}
