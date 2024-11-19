using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action OnTurnPassButtonClicked;
    public Action OnCardSelectEndButtonClicked;

    public Button TESTTURNBUTTON;
    public Button TESTCARDBUTTON;

    public TMP_Text DEBUGTEXT;

    public static UIManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance is not null)
            Destroy(this);
        Instance = this;
    }

    private void Start()
    {
        TESTCARDBUTTON.onClick.AddListener(() =>
        {
            OnCardSelectEndButtonClicked?.Invoke();
        });
        TESTTURNBUTTON.onClick.AddListener(() =>
        {
            OnTurnPassButtonClicked?.Invoke();
        });
    }

    public void SetUI(BattleManager.State state)
    {
        switch (state)
        {
            case BattleManager.State.Idle:
                DEBUGTEXT.text = "Enemy Turn";
                break;
            case BattleManager.State.WaitForCard:
                DEBUGTEXT.text = "Waiting Card";
                break;
            case BattleManager.State.WaitForTarget:
                DEBUGTEXT.text = "Waiting Target";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}