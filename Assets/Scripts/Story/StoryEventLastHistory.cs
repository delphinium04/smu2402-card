using UnityEngine;

namespace Story
{
    [CreateAssetMenu(fileName = "LastHistory", menuName = "Story/Event/LastHistory", order = 1)]
    public class StoryEventLastHistory : StoryEventData
    {
        public override void OnAcceptClicked()
        {
        }

        public override void OnDenyClicked()
        {
        }
    }
}
