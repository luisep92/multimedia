using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button prev;
    [SerializeField] TMP_Text txtDiff;

    // Start is called before the first frame update
    void Start()
    {
        int dif = GameManager.Instance.Difficulty;
        txtDiff.text = dif.ToString();
        if (dif >= 3)
            next.interactable = false;
        else if (dif <= 1)
            prev.interactable = false;
    }

    float deltaTime;
    // Update is called once per frame
    void Update()
    {

    }

    public void NextBtn()
    {
        int currentDiff = Int32.Parse(txtDiff.text);
        currentDiff++;
        GameManager.Instance.Difficulty = currentDiff;
        txtDiff.text = currentDiff.ToString();
        if (currentDiff >= 3)
            next.interactable = false;
        prev.interactable = true;
    }

    public void PrevBtn()
    {
        int currentDiff = Int32.Parse(txtDiff.text);
        currentDiff--;
        GameManager.Instance.Difficulty = currentDiff;
        txtDiff.text = currentDiff.ToString();
        if (currentDiff <= 1)
            prev.interactable = false;
        next.interactable = true;
    }
}
