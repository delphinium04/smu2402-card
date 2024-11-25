using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI: MonoBehaviour
{
    public GameObject _bg;
    
    public void OnStartClicked()
    {
        _bg.GetComponent<Image>().DOFade(0, 1).SetEase(Ease.InQuad);
        Invoke("LoadMapScene", 1.5f);
    }

    void LoadMapScene()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }

    public void OnEndClicked()
    {
        Application.Quit();
    }
}
