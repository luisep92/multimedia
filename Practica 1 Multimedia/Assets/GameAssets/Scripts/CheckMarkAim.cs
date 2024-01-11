using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMarkAim : MonoBehaviour
{
    void Start()
    {
        GetComponent<Toggle>().isOn = GameManager.Instance.PlayerAim;
    }
}
