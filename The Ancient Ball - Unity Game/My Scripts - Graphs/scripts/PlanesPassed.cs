using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanesPassed : MonoBehaviour {

    private float planesPass = 0.0f;
    public Text scoreText;


    private bool isDead = false;
    public DeathMenu deathMenu;

	void Start ()
    {
    }
	
	void Update () {
        if (isDead)
            return;

        planesPass = ((transform.position.z - 25) / 10);
        if (planesPass < 0)
            planesPass = 0;

       scoreText.text = ((int)planesPass).ToString();
	}

  

    public void OnDeath()
    {
        isDead = true;

        if(PlayerPrefs.GetFloat("Highscore")<planesPass)
           PlayerPrefs.SetFloat("Highscore", planesPass);

        deathMenu.ToggleEndMenu(planesPass);
    }


}
