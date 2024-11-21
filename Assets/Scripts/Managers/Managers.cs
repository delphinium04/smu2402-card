using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using TMPro.EditorUtilities;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _sInstance;

    static Managers Instance
    {
        get
        {
            if (_sInstance == null) Init();
            return _sInstance;
        }
    }

    readonly MapManager _map = new MapManager();
    readonly ResourceManager _resource = new ResourceManager();
    readonly BattleManager _battle = new BattleManager();
    readonly GameManager _game = new GameManager();
    
    readonly BgImageManager _bgImage = new BgImageManager();

    public static ResourceManager Resource => Instance._resource;
    public static BattleManager Battle => Instance._battle;
    public static GameManager Game => Instance._game;
    public static MapManager Map => Instance._map;

    public static BgImageManager Background => Instance._bgImage;

    void Start()
    {
        Init();

        Game.Start();
    }

    void Update()
    {
        Game.Update();
    }

    static void Init()
    {
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
        _sInstance?.StartCoroutine(coroutine);
    }
}