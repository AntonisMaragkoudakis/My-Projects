using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu1 : MonoBehaviour
{

    public Text obstaclePassText;
    public Text positionText;


    void Start()
    {
        gameObject.SetActive(false);
    }


    void Update()
    {

    }

    public void ToggleEndMenu1(float planesPass)
    {
        gameObject.SetActive(true);
        obstaclePassText.text = ((int)planesPass).ToString();
        positionText.text = ((int)planesPass).ToString();
        //isShowned = true;
    }

    public void Rastart1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackMenu1()
    {
        SceneManager.LoadScene("Menu");
    }

}
