using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This is the singleton of our game, in this object we will put the stuff that we will move between scenes and the data we want to store

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
