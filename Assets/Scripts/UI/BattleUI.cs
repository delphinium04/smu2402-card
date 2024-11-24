using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class BattleUI : UIBase
{
    public Action OnCardConfirmBtnClicked;
    public Action OnPassBtnClicked;

    enum Buttons
    {
        PassButton,
        ConfirmButton
    }

    enum Texts
    {
        MapText,
        DebugText
    }

    readonly Dictionary<Type, Object[]> _objects = new Dictionary<Type, Object[]>();


    protected void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        
        GetButton(Buttons.ConfirmButton).onClick.AddListener(() => { OnCardConfirmBtnClicked?.Invoke(); });
        GetButton(Buttons.PassButton).onClick.AddListener(() => { OnPassBtnClicked?.Invoke(); });
    }

    TMP_Text GetText(Texts t)
        => Get<TMP_Text>((int)t);

    Button GetButton(Buttons b)
        => Get<Button>((int)b);

    public void SetUI(BattleManager.State state)
    {
        switch (state)
        {
            case BattleManager.State.Idle:
                GetText(Texts.DebugText).text = "Enemy Turn";
                break;
            case BattleManager.State.WaitForCard:
                GetText(Texts.DebugText).text = "카드를 골라주세요";
                break;
            case BattleManager.State.WaitForTarget:
                GetText(Texts.DebugText).text = "타겟을 골라주세요";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    
    public void SetTitle(string t)
    {
        GetText(Texts.MapText).text = t;
    }
}