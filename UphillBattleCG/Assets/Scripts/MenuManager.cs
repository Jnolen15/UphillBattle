using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject LostScreen;
    [SerializeField] private GameObject WonScreen;
    [SerializeField] private GameObject Tut1;
    [SerializeField] private GameObject Tut2;
    [SerializeField] private GameObject Tut3;
    [SerializeField] private GameObject Tut4;

    public void OpenMain()
    {
        StartScreen.SetActive(true);
    }

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

    public void CloseWon()
    {
        WonScreen.SetActive(false);
    }

    public void OpenTut(int page)
    {
        Tut1.SetActive(false);
        Tut2.SetActive(false);
        Tut3.SetActive(false);
        Tut4.SetActive(false);

        if(page == 1) Tut1.SetActive(true);
        else if(page == 2) Tut2.SetActive(true);
        else if(page == 3) Tut3.SetActive(true);
        else if(page == 4) Tut4.SetActive(true);
    }

}
