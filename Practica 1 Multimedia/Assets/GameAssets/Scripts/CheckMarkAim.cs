using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Luis Escolano Piquer
// Controla el toogle de elegir si apuntar o no

public class CheckMarkAim : MonoBehaviour
{
    void Start()
    {
        GetComponent<Toggle>().isOn = GameManager.Instance.PlayerAim;
    }
}
