using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
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

    public void ToggleWinMenu(float planesPass)
    {
        gameObject.SetActive(true);
        obstaclePassText.text = ((int)planesPass).ToString();
        positionText.text = ((int)planesPass).ToString();
    }

    public void RastartAfterWin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackMenuAfterWin()
    {
        SceneManager.LoadScene("Menu");
    }

}
