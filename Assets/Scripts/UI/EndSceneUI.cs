using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneUI : MonoBehaviour
{
    public void OnRetry()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
