using System;
using System.Collections.Generic;
using Effect;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

public class EntityUI : MonoBehaviour
{
    Canvas _localCanvas; // World Space로 설정된 Canvas
    public Transform _parentT; // 플레이어의 Transform

    public RectTransform _panel;
    public RectTransform _hpGauge;
    public RectTransform _effectRoot;

    Dictionary<BaseEffect, GameObject> _effectPairs;

    void Awake()
    {
        _localCanvas = GetComponent<Canvas>();
        _parentT = transform.parent;
        _effectPairs = new Dictionary<BaseEffect, GameObject>();
    }

    void Start()
    {
        if (GetComponentInParent<PlayerController>() != null)
        {
            PlayerController.Instance.OnHpChanged += UpdateHp;
            PlayerController.Instance.GetComponent<EffectManager>().OnEffectAdded += AddEffect;
            PlayerController.Instance.GetComponent<EffectManager>().OnEffectRemoved += RemoveEffect;
        }
        else if (GetComponentInParent<BaseEnemy>() != null)
        {
            var enemyParent = GetComponentInParent<BaseEnemy>();
            enemyParent.OnHpChanged += UpdateHp;
            enemyParent.GetComponent<EffectManager>().OnEffectAdded += AddEffect;
            enemyParent.GetComponent<EffectManager>().OnEffectRemoved += RemoveEffect;
        }
        else
            Debug.LogError("No PlayerController or BaseEnemy");

        _localCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        _localCanvas.worldCamera = Camera.main;
        _hpGauge = _localCanvas.transform.GetChild(0).transform as RectTransform;
    }

    void LateUpdate()
    {
        Vector3 targetPos =_parentT.position + Vector3.up * 2; // 위로 오프셋을 줌
        _panel.position = new Vector3(targetPos.x, targetPos.y, 0);
        _panel.rotation = Quaternion.identity;
    }


    void UpdateHp(int hp, int maxHp)
    {
        Slider s = GetComponentInChildren<Slider>();
        s.value = (float)hp / maxHp;
    }

    void AddEffect(BaseEffect e)
    {
        GameObject g = new GameObject();
        Debug.Log(e.EffectIcon);
        g.AddComponent<Image>().sprite = e.EffectIcon;
        g.transform.SetParent(_effectRoot);
        g.transform.localScale = Vector3.one;
        _effectPairs.Add(e, g);
    }

    void RemoveEffect(BaseEffect e)
    {
        if (_effectPairs.ContainsKey(e))
        {
            Destroy(_effectPairs[e]);
            _effectPairs.Remove(e);
        }
    }
}