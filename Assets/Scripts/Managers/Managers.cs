using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _sInstance;
    static Managers Instance { get { Init(); return _sInstance; } }

    readonly ResourceManager _resource = new ResourceManager();
    readonly BattleManager _battle = new BattleManager();

    public static ResourceManager Resource => Instance._resource;
    public static BattleManager Battle => Instance._battle;
    public static UIManager UI => UIManager.Instance;
    public static PlayerController Player => PlayerController.Instance;

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (_sInstance != null) return;
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        _sInstance = go.GetComponent<Managers>();
    }

    public static void RunCoroutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(coroutine);
    }
}