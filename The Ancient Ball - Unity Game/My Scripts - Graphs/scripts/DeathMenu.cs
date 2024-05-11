using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {


    public Text scoreText;
    public Text highscoreText;

    void Start () {

        gameObject.SetActive(false);
        highscoreText.text = "" + (int)PlayerPrefs.GetFloat("Highscore");
    }
	

	void Update () {

	}

    public void ToggleEndMenu (float planesPass)
    {
       gameObject.SetActive(true);
       scoreText.text = ((int)planesPass).ToString();
    }

    public void Rastart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackMenu()
    {
       SceneManager.LoadScene("Menu");
    }

}
