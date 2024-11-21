using System;
using Effect;
using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityUI : MonoBehaviour
{
    Canvas localCanvas; // World Space로 설정된 Canvas
    public Transform parentT; // 플레이어의 Transform

    private RectTransform rectTransform;

    void Awake()
    {
        localCanvas = GetComponent<Canvas>();
        parentT = transform.parent;
    }

    void Start()
    {
        localCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        localCanvas.worldCamera = Camera.main;
        rectTransform = localCanvas.transform.GetChild(0).transform as RectTransform;

        UpdateUIPosition();
    }

    void UpdateUIPosition()
    {
        localCanvas.worldCamera = Camera.main;
        rectTransform.position = parentT.position + Vector3.up * 2; // 위로 오프셋을 줌
        rectTransform.rotation = Quaternion.identity;
    }

    public void UpdateHp(int hp, int maxHp)
    {
        Slider s = GetComponentInChildren<Slider>();
        s.value = (float) hp/maxHp;
        UpdateUIPosition();
    }

    public void UpdateStatus(BaseEffect e)
    {
        
    } 
}