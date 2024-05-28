using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_skr : MonoBehaviour
{
    public GameObject menu1;
    public GameObject nast;
    private int level;

    public void Pl()
    {
        level=1;
        PlayerPrefs.SetInt("LVL",level);
        PlayerPrefs.Save();
        SceneManager.LoadScene(PlayerPrefs.GetInt("LVL"));
    }
    public void Pl1()
    {
        SceneManager.LoadScene(0);
    }
    public void nast0()
    {
        menu1.SetActive(false);
        nast.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void cotinu(){
        level = PlayerPrefs.GetInt("LVL");
        SceneManager.LoadScene(level);
    }
}
