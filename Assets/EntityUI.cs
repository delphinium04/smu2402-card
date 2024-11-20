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
        localCanvas.worldCamera = Camera.main;
        rectTransform = localCanvas.transform.GetChild(0).transform as RectTransform;

        UpdateUIPosition();
    }

    void UpdateUIPosition()
    {
        // 플레이어의 월드 좌표를 스크린 좌표로 변환
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(parentT.position);

        // 스크린 좌표를 Canvas의 RectTransform 좌표로 변환
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)localCanvas.transform,
            screenPosition, Camera.main, out canvasPosition);

        // Canvas의 RectTransform 위치 업데이트
        rectTransform.localPosition = canvasPosition + Vector2.up * 100;
    }

    public void UpdateHP(int hp, int maxHp)
    {
        Slider s = GetComponentInChildren<Slider>();
        s.value = (float) hp/maxHp;
    }

    public void UpdateStatus(BaseEffect e)
    {
        
    } 
}