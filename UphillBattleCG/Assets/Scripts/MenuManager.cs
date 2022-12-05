using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject LostScreen;
    [SerializeField] private GameObject WonScreen;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject Tut1;
    [SerializeField] private GameObject Tut2;
    [SerializeField] private GameObject Tut3;
    [SerializeField] private GameObject Tut4;
    [SerializeField] private AudioMixer EffectsMixer;
    [SerializeField] private AudioMixer MusicMixer;
    [SerializeField] private bool gamePaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        gamePaused = !gamePaused;

        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        PauseScreen.SetActive(gamePaused);
    }

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

    public void Restart()
    {
        TogglePause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();   
    }

    public void SetEffectVolume(float sliderVal)
    {
        EffectsMixer.SetFloat("EffectVol", Mathf.Log10(sliderVal) * 20);
    }
    
    public void SetMusicVolume(float sliderVal)
    {
        MusicMixer.SetFloat("MusicVol", Mathf.Log10(sliderVal) * 20);
    }

}
