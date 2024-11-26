using DG.Tweening;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    void Start()
    {
        SetFadeIn(1);
    }

    // 검 -> 화면
    public void SetFadeIn(float time)
    {
        GetComponent<SpriteRenderer>().DOFade(0, time).onComplete = () => { GetComponent<SpriteRenderer>().enabled = false; };
    }

    // 화면 -> 검
    public void SetFadeOut(float time)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().DOFade(1, time);
    }
}
