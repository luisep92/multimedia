using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum Direction { RIGHT = 1, LEFT = -1 }

public static class Utils
{
    public static void EnableDisableGameObject(GameObject go)
    {
        if (go.activeInHierarchy)
            go.SetActive(false);
        else
            go.SetActive(true);
    }
}
