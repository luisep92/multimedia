using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Luis Escolano Piquer
// Interfaz de usuario del jefe

public class UIBoss : MonoBehaviour
{
    [SerializeField] private TMP_Text txtHP;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void OnDamage(int hp)
    {
        txtHP.text = hp.ToString();
        anim.Play("BossHp");
    }

    public void OnDie()
    {
        var images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
            image.color = new Color(1, 1, 1, 0.2f);
    }
}
