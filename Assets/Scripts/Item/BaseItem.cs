using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public enum ItemType {Accessory, Companion}
    public abstract class BaseItem : ScriptableObject
    {
        [SerializeField]
        private ItemType itemType;
        public ItemType ItemType => itemType;
        
        [SerializeField]
        private string itemName;
        public string ItemName => itemName;

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;
        
        [SerializeField]
        private string description;
        public string Description => description;

        protected GameObject Target;
        
        public void Init(GameObject target)
        {
            if (Target != null)
            {
                Debug.LogError($"{GetType().Name}.Init: Target already set!");
                return;
            }

            Target = target;

            Enable();
            // target.ItemManager.AddItem(this);
        }
        
        protected abstract void Enable();
        // Called from ItemManager
        public abstract void Disable();
    }
}
