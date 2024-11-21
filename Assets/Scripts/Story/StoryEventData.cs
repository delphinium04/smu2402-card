using UnityEngine;

namespace Story
{
    public abstract class StoryEventData : ScriptableObject
    {
        public string storyEventName;
        [TextArea]public string storyDescription;
        
        [Tooltip("수락 미리보기")] [TextArea]
        public string storyAcceptPreview; 
        [Tooltip("거절 미리보기")] [TextArea]
        public string storyDenyPreview; 
        [Tooltip("수락 후 내용")] [TextArea]
        public string storyAcceptDescription;
        [Tooltip("거절 후 내용")] [TextArea]
        public string storyDenyDescription;

        public abstract void OnAcceptClicked();
        public abstract void OnDenyClicked();
    }
}
