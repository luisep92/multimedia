using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Luis Escolano Piquer

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button playBtn;
    public void BtnPlay()
    {
        playBtn.interactable = false;
        GameManager.Instance.LoadScene("RollABall");
    }
}
