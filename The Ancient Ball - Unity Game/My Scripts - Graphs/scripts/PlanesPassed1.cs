using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanesPassed1 : MonoBehaviour
{
    private float planesPass = 0.0f;
    public Text score1Text;


    private bool isDead = false;
    public DeathMenu1 deathMenu1;
    public WinMenu winMenu;


    void Start()
    {
    }

    void Update()
    {
        if (isDead)
            return;

        planesPass = ((transform.position.z - 25) / 10);
        if (planesPass < 0)
            planesPass = 0;
        score1Text.text = ((int)planesPass).ToString();
    }



    public void OnDeath1()
    {
        isDead = true;
        deathMenu1.ToggleEndMenu1(planesPass); 
    }

    public void OnWin()
    {
        isDead = true;
        winMenu.ToggleWinMenu(planesPass);
    }


}

