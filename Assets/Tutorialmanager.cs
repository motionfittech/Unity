using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialmanager : MonoBehaviour
{
    public void startGame()
    {
        PlayerPrefs.SetInt("firsttime", 1);
        Application.LoadLevel(3);
    }
}
