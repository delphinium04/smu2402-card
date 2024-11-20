using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIManager : Singleton<UIManager>
{
    Canvas _rootCanvas;
    public Action OnCardConfirmBtnClicked;
    public Action OnPassBtnClicked;

    enum Buttons
    {
        PassButton,
        ConfirmButton
    }

    enum Texts
    {
        DebugText
    }

    readonly Dictionary<Type, Object[]> _objects = new Dictionary<Type, Object[]>();


    protected override void Awake()
    {
        base.Awake();
        
        _rootCanvas = GameObject.Find("@UI_Root").GetComponent<Canvas>();
        
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        
        GetButton(Buttons.ConfirmButton).onClick.AddListener(() => { OnCardConfirmBtnClicked?.Invoke(); });
        GetButton(Buttons.PassButton).onClick.AddListener(() => { OnPassBtnClicked?.Invoke(); });
    }

    void Bind<T>(Type type) where T : Object
    {
        string[] names = Enum.GetNames(type);
        Object[] objects = new Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            objects[i] = _rootCanvas.transform.Find(names[i]).GetComponent<T>();
            if (objects[i] == null)
                Debug.LogError($"Could not find object {names[i]}");
        }
    }

    T Get<T>(int idx) where T : Object
    {
        Object[] objects;
        if (!_objects.TryGetValue(typeof(T), out objects)) return null;
        return objects[idx] as T;
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
                GetText(Texts.DebugText).text = "Waiting Card";
                break;
            case BattleManager.State.WaitForTarget:
                GetText(Texts.DebugText).text = "Waiting Target";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}