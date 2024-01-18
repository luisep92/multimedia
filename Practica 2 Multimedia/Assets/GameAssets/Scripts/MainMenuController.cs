using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void BtnPlay()
    {
        SceneManager.LoadScene("RollABall");
    }
}
