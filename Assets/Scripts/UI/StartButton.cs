using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Startbutton()
    {
        SceneManager.LoadScene("New Scene");
        //메인화면 이후 시작될 씬 이름으로 바꾸면 됩니다.
    }
}
