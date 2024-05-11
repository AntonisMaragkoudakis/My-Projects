using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public Text highscoreText;

	// Use this for initialization
	void Start () {
        highscoreText.text = "" + (int)PlayerPrefs.GetFloat("Highscore");
		
	}
	
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToGame1()
    {
        SceneManager.LoadScene("Game1");
    }

}
