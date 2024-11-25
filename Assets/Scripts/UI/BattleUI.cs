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

    const string EnemyTurnNext = "적 턴입니다";
    const string SelectCardText = "카드를 골라주세요";
    const string SelectTargetText = "타겟을 골라주세요";

    readonly Dictionary<Type, Object[]> _objects = new Dictionary<Type, Object[]>();

    enum Buttons
    {
        PassButton,
        ConfirmButton,
        WinOKButton
    }

    enum Texts
    {
        MapText,
        DebugText
    }

    enum Images
    {
        WinPanel
    }

    protected void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        
        Get<Image>((int)Images.WinPanel).gameObject.SetActive(false);
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        GetButton(Buttons.ConfirmButton).onClick.AddListener(() => OnCardConfirmBtnClicked?.Invoke());
        GetButton(Buttons.PassButton).onClick.AddListener(() => OnPassBtnClicked?.Invoke());
        GetButton(Buttons.WinOKButton).onClick.AddListener(() => Managers.Game.EndBattle());
    }

    TMP_Text GetText(Texts textType) => Get<TMP_Text>((int)textType);
    Button GetButton(Buttons buttonType) => Get<Button>((int)buttonType);

    public void SetUI(BattleManager.State state)
    {
        switch (state)
        {
            case BattleManager.State.Idle:
                GetText(Texts.DebugText).text = EnemyTurnNext;
                break;
            case BattleManager.State.WaitForCard:
                GetText(Texts.DebugText).text = SelectCardText;
                break;
            case BattleManager.State.WaitForTarget:
                GetText(Texts.DebugText).text = SelectTargetText;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void SetTitle(string t)
    {
        GetText(Texts.MapText).text = t;
    }

    public void SetWinUI()
    {
        Get<Image>((int)Images.WinPanel).gameObject.SetActive(true);
    }
}