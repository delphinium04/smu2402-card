using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI: MonoBehaviour
{

    public void OnStartClicked()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }

    public void OnEndClicked()
    {
        Application.Quit();
    }
}
