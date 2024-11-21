using System;
using UnityEngine;
using UnityEngine.UI;

public class BgImageManager
{
    public enum ImageType
    {
        Title,
        Map,
        Battle
    }

    SpriteRenderer _renderer;
    
    public void SetImage(ImageType type)
    {
        if (!_renderer)
        {
            _renderer = new GameObject("@BG").AddComponent<SpriteRenderer>();
            _renderer.sortingOrder = -1000;
        }
        
        Sprite s = Resources.Load<Sprite>("BG/default");
        switch (type)
        {
            case ImageType.Title:
                s = Resources.Load<Sprite>("BG/default");
                break;
            case ImageType.Map:
                s = Resources.Load<Sprite>("BG/first-map");
                break;
            case ImageType.Battle:
                s = Resources.Load<Sprite>("BG/battle");
                break;
            default:
                s = Resources.Load<Sprite>("BG/default");
                break;
        }
        _renderer.sprite = s;
    }
}