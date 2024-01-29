using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject levelsPanel;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBtn()
    {
        Utils.EnableDisableGameObject(levelsPanel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
