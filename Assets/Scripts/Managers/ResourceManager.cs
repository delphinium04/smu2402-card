using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Object = UnityEngine.Object;
using Type = Enemy.Type;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
        => Resources.Load<T>(path);
    
    public T[] LoadAll<T>(string path) where T : Object
        => Resources.LoadAll<T>(path);

    /// <summary>
    /// Instantiate Object
    /// </summary>
    /// <param name="path">Resources/Prefabs/{path}</param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab != null) return Object.Instantiate(prefab, parent);
        Debug.Log($"Failed to load prefab : {path}");
        return null;
    }
    
    public BaseEnemy GetEnemy(EnemyData d)
    {
        GameObject go = Instantiate("BaseEnemy");

        BaseEnemy component = d.Type switch
        {
            Type.Boatswain => go.AddComponent<Boatswain>(),
            Type.Cleaner => go.AddComponent<Cleaner>(),
            Type.CombatSailor => go.AddComponent<CombatSailor>(),
            Type.PirateCaptain => go.AddComponent<PirateCaptain>(),
            Type.PirateLieutenant => go.AddComponent<PirateLieutenant>(),
            Type.PlunderingCrew => go.AddComponent<PlunderingCrew>(),
            Type.SeaCrow => go.AddComponent<SeaCrow>(),
            Type.WatchfulSallor => go.AddComponent<WatchfulSailor>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        component.Init(d);
        return component;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}