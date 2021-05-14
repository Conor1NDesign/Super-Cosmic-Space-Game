using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Player Index: 0 for P1, 1 for P2, etc.")]
    public int playerIndex;

    public GameObject playerJoinIcon;
    public GameObject playerJoinedIcon;
    public GameObject playerRenderGreyed;
    public GameObject playerRenderFull;

    public bool activePlayer;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void UpdateJoinedUI()
    {
        activePlayer = true;
        playerJoinIcon.SetActive(false);
        playerJoinedIcon.SetActive(true);

        playerRenderGreyed.SetActive(false);
        playerRenderFull.SetActive(true);
    }
}
