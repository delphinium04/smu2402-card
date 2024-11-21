using System;
using Story;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI
{
    public class StoryEventUI : UIBase
    {
        enum Texts
        {
            EventTitleText,
            EventScriptText,
            DenyPreviewText,
            AcceptPreviewText,
            ResultText
        }

        enum Buttons
        {
            AcceptButton,
            DenyButton,
            ResultButton
        }

        enum Images
        {
            EventImage
        }

        enum Panels
        {
            ResultPanel,
        }

        void BindAll()
        {
            Bind<TMP_Text>(typeof(Texts));
            Bind<Button>(typeof(Buttons));
            Bind<Image>(typeof(Images));
            Bind<Transform>(typeof(Panels));
        }

        public void UpdateUI(StoryEventData data)
        {
            if (Objects.Count == 0) BindAll();

            GetText(Texts.EventTitleText).text = data.storyEventName;
            GetText(Texts.EventScriptText).text = data.storyDescription;
            GetText(Texts.AcceptPreviewText).text = data.storyAcceptPreview;
            GetText(Texts.DenyPreviewText).text = data.storyDenyPreview;
            GetButton(Buttons.AcceptButton).onClick.AddListener(() =>
            {
                data.OnAcceptClicked();
                Get<Transform>((int)Panels.ResultPanel).gameObject.SetActive(true);
                GetText(Texts.ResultText).text = data.storyAcceptDescription;
            });
            GetButton(Buttons.DenyButton).onClick.AddListener(() =>
            {
                data.OnDenyClicked();
                Debug.Log("Denyclicked");
                Get<Transform>((int)Panels.ResultPanel).gameObject.SetActive(true);
                GetText(Texts.ResultText).text = data.storyDenyDescription;
            });
            GetButton(Buttons.ResultButton).onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
            
            Get<Transform>((int)Panels.ResultPanel).gameObject.SetActive(false);
        }

        TMP_Text GetText(Texts text)
            => Get<TMP_Text>((int)text);

        Button GetButton(Buttons button)
            => Get<Button>((int)button);
    }
}