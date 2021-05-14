using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject exitPrompt;

    public MenuController menuController01;
    public MenuController menuController02;
    public MenuController menuController03;
    public MenuController menuController04;


    public void PlayTheGame()
    {
        if (menuController01.activePlayer == true && menuController02.activePlayer == true && menuController03.activePlayer == true && menuController04.activePlayer == true)
        {
            SceneManager.LoadScene("Main_ShipScene");
        }
    }

    public void QuitPrompt()
    {
        exitPrompt.SetActive(true);
    }

    public void QuitConfirm()
    {
        Application.Quit();
    }

    public void CancelQuitting()
    {
        exitPrompt.SetActive(false);
    }
}
