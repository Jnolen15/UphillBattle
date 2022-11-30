using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject LostScreen;
    [SerializeField] private GameObject WonScreen;

    public void CloseMain()
    {
        StartScreen.SetActive(false);
    }

    public void OpenLost()
    {
        LostScreen.SetActive(true);
    }

    public void OpenWon()
    {
        WonScreen.SetActive(true);
    }

}
