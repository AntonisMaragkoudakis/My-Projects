using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void doQuit()
    {
        Debug.Log("Game has quit");
        Application.Quit();
    }
}
