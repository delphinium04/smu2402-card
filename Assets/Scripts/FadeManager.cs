using UnityEngine;

public class FadeManager : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<UnityEngine.UI.Image>().CrossFadeAlpha(0, 1f, false);
        Invoke("Destroy", 1.5f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
